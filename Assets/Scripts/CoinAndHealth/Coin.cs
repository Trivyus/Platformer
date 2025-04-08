using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : Item
{
    public event Action<Coin> Collected;

    public void Collect()
    {
        Collected?.Invoke(this);
        Destroy(gameObject);
    }
}
