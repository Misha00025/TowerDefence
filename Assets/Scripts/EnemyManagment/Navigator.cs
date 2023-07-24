using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField] private List<Vector2>[] _routes;
    private GameBoard _gameBoard;

    public IReadOnlyList<Vector2> Route
    {
        get 
        {
            System.Random rand = new System.Random();
            int index = rand.Next(0, _routes.Length);
            return _routes[index]; 
        }
    }

    public void Initialize(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
        _gameBoard.LoadComplete.AddListener(GenerateRoute);
    }

    private void GenerateRoute()
    {        
        _routes = GenerateRoutesTo(_gameBoard.TargetCell, new HashSet<Cell>()).ToArray();
        Debug.Log(_routes.Length);
        
        foreach (var route in _routes)
        {
            string routeStr = "";
            foreach (var cell in route)
            {
                routeStr += cell.ToString();
            }
            Debug.Log(routeStr);
        }
    }

    private List<List<Vector2>> GenerateRoutesTo(Cell targetCell, HashSet<Cell> passedCells)
    {
        if (targetCell == null)
            return null;

        passedCells.Add(targetCell);

        List<List<Vector2>> routes = new List<List<Vector2>>();

        foreach (Cell neighbour in targetCell.Neighbours)
        {
            //Debug.Log($"Current: {targetCell.Position}; Neighbour: {neighbour.Position}");
            if (passedCells.Contains(neighbour)) continue;

            List<List<Vector2>> neighbourRoutes = GenerateRoutesTo(neighbour, passedCells.ToHashSet());
            if (neighbourRoutes == null) continue;

            routes.AddRange(neighbourRoutes);
        }

        if (routes.Count == 0)
        {
            //if (!_gameBoard.SpawnCellPositions.Contains(targetCell.Position)) return null;
            if (targetCell.Neighbours.Count > 1) return null;
            routes.Add(new List<Vector2>());
        }

        Vector2 targetCellWorldPosition = _gameBoard.Grid.GetCellCenterWorld((Vector3Int)targetCell.Position);

        foreach (List<Vector2> route in routes)
        {
            route.Add(targetCellWorldPosition);
        }

        return routes;
    }

    private Vector2[] ClearRoute(Vector2[] route)
    {
        List<Vector2> newRoute = new List<Vector2>(route.Length);
        Vector2 lastDirection = Vector2.zero;
        Vector2 lastPoint = route[0];
        foreach (var point in route)
        {
            Vector2 direction = (point - lastPoint).normalized;
            if (Vector2.Distance(direction, lastDirection) > 0.1f)
                newRoute.Add(lastPoint);
            lastPoint = point;
        }
        newRoute.Add(lastPoint);
        return newRoute.ToArray();
    }
}
