using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cell
{
    private TileBase _tileBase;
    private Vector2Int _position;
    private Dictionary<Vector2Int, Cell> _initializedCells;
    private List<Cell> _neighbours = new List<Cell>();

    private Cell(TileBase tileBase, Dictionary<Vector2Int, Cell> initializedCells, Vector2Int position)
    {
        _tileBase = tileBase;
        _initializedCells = initializedCells;
        _position = position;

        _initializedCells.Add(position, this);
    }

    public IReadOnlyList<Cell> Neighbours => _neighbours;

    public Vector2Int Position => _position;

    public static Cell GetInitializedOrCreate(TileBase tileBase, Vector2Int position, Dictionary<Vector2Int, Cell> initializedCells)
    {
        if (initializedCells.ContainsKey(position))
            return initializedCells[position];

        Cell newCell = new Cell(tileBase, initializedCells, position);
        // Debug.Log($"New Cell Created At Position: {position} ");
        return newCell;
    }

    public void DefineSurroundingTiles(Tilemap tilemap)
    {
        Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.left, Vector2Int.right, Vector2Int.down };

        foreach (var direction in directions)
        {
            Cell neighbour = FindNeighbourOnDirection(tilemap, direction);
            if (neighbour != null)
                _neighbours.Add(neighbour);
        }
    }

    private Cell FindNeighbourOnDirection(Tilemap tilemap, Vector2Int direction)
    {
        Vector2Int neighbourPosition = (_position + direction);
        TileBase neighbourTileBase = tilemap.GetTile((Vector3Int)neighbourPosition);

        if (neighbourTileBase == null || neighbourTileBase != _tileBase)
            return null;

        return Cell.GetInitializedOrCreate(neighbourTileBase, neighbourPosition, _initializedCells);
    }
}
