using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPack : Item
{
    [SerializeField] private float _healAmount = 30;

    public event Action<HealthPack> Collected;

    public float HealAmount => _healAmount;

    public void Collect()
    {
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}