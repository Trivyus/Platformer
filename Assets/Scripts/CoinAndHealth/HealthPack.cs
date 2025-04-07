using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPack : MonoBehaviour, ICollectible
{
    [SerializeField] private float _healAmount = 30;
    public float HealAmount => _healAmount;

    public event Action<ICollectible> Collected;

    public void Collect(ICollectibleVisitor visitor)
    {
        visitor.Visit(this);
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}