using System.Collections.Generic;

public class Rewarder
{
    private PlayerWallet _wallet;
    public Rewarder(List<Wave> waves, PlayerWallet wallet)
    {
        _wallet = wallet;
        foreach (Wave wave in waves)
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
