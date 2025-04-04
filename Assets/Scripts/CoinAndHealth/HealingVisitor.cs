using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingVisitor : ICollectibleVisitor
{
    private Health _playerHealth;

    public HealingVisitor(Health playerHealth)
    {
        _playerHealth = playerHealth;
    }

    public void Visit(Coin coin)
    {
        Debug.Log("Coin collected!");
    }

    public void Visit(HealthPack healthPack)
    {
        _playerHealth.Recover(healthPack.HealAmount);
    }
}
