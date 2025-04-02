using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public event Action<Coin> CoinCollected;

    public void Collect()
    {
        CoinCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
