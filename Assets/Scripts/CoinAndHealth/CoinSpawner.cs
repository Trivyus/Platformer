using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Spawner
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _maxCoins = 3;

    private Dictionary<Coin, SpawnPoint> _spawnedCoins;

    private void Awake()
    {
        _spawnedCoins = new ();
    }

    private void Start()
    {
        SpawnCoins();
    }

    public void SpawnCoins()
    {
        for (int i = 0; i < _maxCoins; i++)
            SpawnCoin();
    }

    private void SpawnCoin()
    {
        SpawnPoint freePoint = GetSpawnPosition();

        Coin newCoin = Instantiate(_coinPrefab, freePoint.transform.position, Quaternion.identity);
        newCoin.Collected += ItemCollected;

        _spawnedCoins.Add(newCoin, freePoint);
    }

    private void ItemCollected(Item collectedItem)
    {
        if (collectedItem is Coin coin)
        {
            _spawnedCoins[coin].ChangePlaceStatus();
            _spawnedCoins.Remove(coin);
            coin.Collected -= ItemCollected;
            SpawnCoin();
        }
    }

    private void OnDisable()
    {
        foreach (var spawnedCoin in _spawnedCoins)
            spawnedCoin.Key.Collected -= ItemCollected;
    }
}
