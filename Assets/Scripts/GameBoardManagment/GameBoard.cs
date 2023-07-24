using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class GameBoard : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private List<GameObject> _objects = new List<GameObject>(100);
    [SerializeField] private GameObject _target;
    [SerializeField] private TileBase _waterTileBase;
    [SerializeField] private TileBase _foundationTileBase;
    [SerializeField] private List<Vector2Int> _spawnPointCellPositions = new List<Vector2Int>();

    private bool _initialized = false;
    private Grid _grid = null;

    private Vector2[] _water;
    private Dictionary<Vector2Int, GameObject> _buildings = new Dictionary<Vector2Int, GameObject>();
    private Dictionary<Vector2Int, Cell> _initializedCells = new Dictionary<Vector2Int, Cell>();

    public UnityEvent LoadComplete { get; private set; } = new UnityEvent();
    public Grid Grid => _grid;
    public Cell TargetCell
    {
        get
        {
            Vector2Int targetCellPosition = (Vector2Int)_grid.WorldToCell(_target.transform.position);
            return _initializedCells.ContainsKey(targetCellPosition) ? _initializedCells[targetCellPosition] : null;
        }
    }
    public IReadOnlyList<Vector2Int> SpawnCellPositions => _spawnPointCellPositions;

       

    public void Initialize()
    {
        if (_initialized) return;
        _grid = GetComponent<Grid>();
        _initialized = true;
        FindCells();
        AlignToGrid();
    }

    private void FindCells()
    {
        foreach (Vector2Int cellPosition in _tilemap.cellBounds.allPositionsWithin)
        {
            TileBase tile = _tilemap.GetTile((Vector3Int)cellPosition);
            if (tile != null)
            {
                if (tile == _waterTileBase)
                {
                    Cell currentCell = Cell.GetInitializedOrCreate(tile, cellPosition, _initializedCells);
                    currentCell.DefineSurroundingTiles(_tilemap);
                }
                if (tile == _foundationTileBase)
                {
                    _buildings.Add(cellPosition, null);
                }
            }
        }
        LoadComplete.Invoke();
    }

#if UNITY_EDITOR
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
#endif

    public Vector2[] GetWaterCells()
    {
        if (_water == null)
            return new Vector2[] { Vector2.zero };
        return (Vector2[])_water.Clone();
    }

    public Vector2 GetTargetPosition()
    {
        return _target.transform.position;
    }

    public void SetOnGrid(GameObject gameObject)
    {
        if (!_initialized) return;
        Vector3Int cellPosition = GetCellPosition(gameObject.transform.position);
        if (_buildings.ContainsKey((Vector2Int)cellPosition))
        {
            _buildings[(Vector2Int)cellPosition] = gameObject;
            _objects.Add(gameObject);
            Vector3 vector = _grid.GetCellCenterWorld(cellPosition);
            gameObject.transform.parent = transform;
            gameObject.transform.position = vector;
        }
        
    }

    public bool IsFreeGround(Vector2 position)
    {
        Vector2Int cellPosition = (Vector2Int)GetCellPosition(position);
        return _buildings.ContainsKey(cellPosition) && _buildings[cellPosition] == null;
    }

    public Vector3Int GetCellPosition(Vector2 worldPosition)
    {
        return _grid.WorldToCell(worldPosition);
    }
}
