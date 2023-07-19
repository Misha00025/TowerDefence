using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingShop : Builder
{
    [SerializeField] private List<TowerBlueprint> _blueprints;

    private PlayerWallet _wallet;

    private int _nextPrice = 0;

    public void Initialize(GameBoard _gameBoard, PlayerWallet wallet)
    {
        base.Initialize(_gameBoard);
        _wallet = wallet;
        foreach (var blueprint in _blueprints)
        {
            blueprint.TowerIntansiated.AddListener((int price) =>
            {
                Debug.Log($"Price = {price}");
                _nextPrice = price;
            });
        }
    }

    public override bool TryBuildTower(Tower tower)
    {
        bool hasMoney = _wallet.Money >= _nextPrice;
        bool sucsess = hasMoney && base.TryBuildTower(tower);
        if (hasMoney && sucsess)
            _wallet.Pay(_nextPrice);
        else if (sucsess)
            Destroy(tower.gameObject);
        return sucsess;
    }
}
