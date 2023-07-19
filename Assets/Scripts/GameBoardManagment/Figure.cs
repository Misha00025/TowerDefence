
using UnityEngine;

public abstract class Figure : MonoBehaviour
{
    private bool _initialized = false;
    private GameBoard _gameBoard = null;

    public void Initialize(GameBoard gameBoard)
    {
        if (_initialized) return;
        _gameBoard = gameBoard;
        _initialized = true;
    }


    public Vector2 GetPosition()
    {
        return Vector2.zero;
    }
}
