using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _coinSpawnPoints;
    [SerializeField] private int _maxCoins = 3;

    private List<Coin> _spawnedCoins;

    private void Awake()
    {
        _spawnedCoins = new List<Coin>();
    }

    private void Start()
    {
        for (int i = 0; i < _maxCoins; i++)
            SpawnCoin();
    }

    private void SpawnCoin()
    {
        Transform[] freePoints = GetSpawnPosition();

        if (freePoints.Length == 0)
            return;

        Transform spawnPoint = freePoints[Random.Range(0, freePoints.Length)];
        Coin newCoin = Instantiate(_coinPrefab, spawnPoint.position, Quaternion.identity);
        newCoin.CoinCollected += OnCoinCollected;

        _spawnedCoins.Add(newCoin);
    }

    private Transform[] GetSpawnPosition()
    {
        var freePoints = _coinSpawnPoints.Where(point =>
            !_spawnedCoins.Any(coin => coin != null && coin.transform.position == point.position)).ToArray();

        return freePoints;
    }

    private void OnCoinCollected(Coin collectedCoin)
    {
        _spawnedCoins.Remove(collectedCoin);

        collectedCoin.CoinCollected -= OnCoinCollected;
        SpawnCoin();
    }
}
