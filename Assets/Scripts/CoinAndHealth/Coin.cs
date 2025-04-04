using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, ICollectible
{
    public event Action<Coin> CoinCollected;

    public void Collect()
    {
        CoinCollected?.Invoke(this);
        Destroy(gameObject);
    }

    public void Accept(ICollectibleVisitor visitor)
    {
        visitor.Visit(this);
    }
}
