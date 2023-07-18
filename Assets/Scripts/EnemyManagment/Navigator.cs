using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField]
    private Vector2[] _route;
    [SerializeField]
    private Vector2 _targetOffset;
    private GameBoard _gameBoard;

    public Vector2[] Route => (Vector2[])_route.Clone();

    public void Initialize(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
        _gameBoard.LoadComplite.AddListener(GenerateRoute);
    }

    private void GenerateRoute()
    {        
        List<Vector2> cells = _gameBoard.GetWaterCells().ToList();        
        _route = GenerateRoute(_gameBoard.GetTargetPosition() + _targetOffset, cells).ToArray();
        _route = ClearRoute(_route);
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

    private List<Vector2> GenerateRoute(Vector2 currentCell, List<Vector2> cells)
    {
        if (cells.Count == 0) 
            return new List<Vector2>();
        Vector2 nextCell = cells[0];
        float minDist = float.MaxValue;
        foreach (Vector2 cell in cells)
        {
            float distance = Vector2.Distance(cell, currentCell);
            if (distance < minDist)
            {
                nextCell = cell;
                minDist = distance;
            }
        }
        cells.Remove(nextCell);
        var result = GenerateRoute(nextCell, cells);
        result.Add(nextCell);
        return result;
    }
}
