using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private int _money = 0;
    [SerializeField] private int _maxMoney = int.MaxValue;

    public UnityEvent MoneyChanged = new UnityEvent(); 
    public int Money => _money;

    public void Pay(int amount)
    {
        Debug.Log($"Amount: {amount}");
        if (amount < 0) return;
        if (amount > Money) return;
        _money -= amount;
        MoneyChanged.Invoke();
    }

    public void GetMoney(int amount)
    {
        if (amount < 0) return;
        _money += amount;
        if (_money > _maxMoney) _money = _maxMoney;
        MoneyChanged.Invoke();
    }
}
