using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour
{
    public event Action<Coin> CoinCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out _))
        {
            Collect();
        }
    }

    private void Collect()
    {
        CoinCollected?.Invoke(this);
        Destroy(gameObject);
    }
}
