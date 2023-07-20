using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Builder : MonoBehaviour
{
    private GameBoard _gameBoard;

    public UnityEvent<Tower> NewTowerBuilded { get; private set; } = new UnityEvent<Tower>();

    public void Initialize(GameBoard gameBoard)
    {
        _gameBoard = gameBoard;
    }

    public virtual bool TryBuildTower(Tower tower)
    {
        if (_gameBoard.IsFreeGround(tower.transform.position))
        {
            _gameBoard.SetOnGrid(tower.gameObject);
            NewTowerBuilded.Invoke(tower);
        }
        else
        {
            Destroy(tower.gameObject);
            return false;
        }
        return true;
    }
}
