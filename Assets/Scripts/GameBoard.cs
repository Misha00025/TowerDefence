using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class GameBoard : MonoBehaviour
{
    private bool _initialized = false;
    private Grid _grid = null;
    [SerializeField]
    private Tilemap _tilemap;
    [SerializeField]
    private List<GameObject> _objects = new List<GameObject>(100);

    [SerializeField]
    private GameObject _target;

    [SerializeField]
    private Vector2[] _water;
    [SerializeField]
    private Vector2[] _ground;

    public UnityEvent LoadComplite { get; private set; } = new UnityEvent();

    public void Initialize()
    {
        if (_initialized) return;
        _grid = GetComponent<Grid>();
        _initialized = true;
        StartCoroutine(FindCells());
    }

    private IEnumerator FindCells()
    {
        yield return new WaitForFixedUpdate();
        Cell[] cells = _tilemap.GetComponentsInChildren<Cell>();
        List<Vector2> water = new List<Vector2>();
        List<Vector2> ground = new List<Vector2>();
        foreach (Cell cell in cells)
        {
            if (cell.CanBuild)
            {
                ground.Add(cell.transform.position);
            }
            if (cell.CanMove)
            {
                water.Add(cell.transform.position);
            }
            Destroy(cell.gameObject);
        }
        _water = water.ToArray();
        _ground = ground.ToArray();
        LoadComplite.Invoke();
    }

    [ContextMenu("Выровнять по сетке")]
    private void AlignToGrid()
    {
        Grid grid = GetComponent<Grid>();
        if (grid == null) return;

        foreach( var obj in _objects)
        {
            var cell = grid.WorldToCell(obj.transform.position);
            var newPosition = grid.GetCellCenterWorld(cell);
            obj.transform.position = newPosition;
        }
    }

    public Vector2[] GetWaterCells()
    {
        return (Vector2[])_water.Clone();
    }

    public Vector2[] GetGrounCells()
    {
        return (Vector2[])_ground.Clone();
    }

    public Vector2 GetTargetPosition()
    {
        return _target.transform.position;
    }

    public void SetOnGrid(GameObject gameObject)
    {
        if (!_initialized) return;

    }
}
