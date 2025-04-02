using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HealthPack : MonoBehaviour
{
    [SerializeField] private float _healAmount = 30;

    public event Action<HealthPack> PackCollected;

    public float HealAmount => _healAmount;

    public void Collect()
    {
        PackCollected?.Invoke(this);
        Destroy(gameObject);
    }
}