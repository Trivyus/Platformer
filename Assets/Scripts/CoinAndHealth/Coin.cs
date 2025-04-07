using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, ICollectible
{
    public event Action<ICollectible> Collected;

    public void Collect(ICollectibleVisitor visitor)
    {
        visitor.Visit(this);
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}
