using System.Collections.Generic;

public class Rewarder
{
    private PlayerWallet _wallet;
    public Rewarder(PlayerWallet wallet)
    {
        _wallet = wallet;
    }

    public void OnWaveStart(Wave wave)
    {
        foreach (var mover in wave.Enemies)
        {
            Enemy enemy = mover.Enemy;
            enemy.Died.AddListener(OnEnemyDied);
        }
    }

    private void OnEnemyDied(int reward)
    {
        _wallet.GetMoney(reward);
    }
}
