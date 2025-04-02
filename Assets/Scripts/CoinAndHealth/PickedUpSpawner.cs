using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PickedUpSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private HealthPack _healthPackPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _maxCoins = 3;
    [SerializeField] private int _maxPackes = 2;

    private List<Coin> _spawnedCoins;
    private List<HealthPack> _spawnedPackes;

    private void Awake()
    {
        _spawnedCoins = new List<Coin>();
        _spawnedPackes = new List<HealthPack>();
    }

    private void Start()
    {
        for (int i = 0; i < _maxCoins; i++)
            SpawnCoin();

        for (int i = 0; i < _maxPackes; i++)
            SpawnHealthPack();
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

    private void SpawnHealthPack()
    {
        Transform[] freePoints = GetSpawnPosition();

        if (freePoints.Length == 0)
            return;

        Transform spawnPoint = freePoints[Random.Range(0, freePoints.Length)];

        HealthPack newPack = Instantiate(_healthPackPrefab, spawnPoint.position, Quaternion.identity);
        newPack.PackCollected += OnPackCollected;

        _spawnedPackes.Add(newPack);
    }

    private Transform[] GetSpawnPosition()
    {
        var freePoints = _spawnPoints.Where(point => 
        point != null && !_spawnedCoins.Any(coin => coin != null && coin.transform.position == point.position) &&
        !_spawnedPackes.Any(pack => pack != null && pack.transform.position == point.position)).ToArray();

        return freePoints;
    }

    private void OnCoinCollected(Coin collectedCoin)
    {
        _spawnedCoins.Remove(collectedCoin);

        collectedCoin.CoinCollected -= OnCoinCollected;
        SpawnCoin();
    }

    private void OnPackCollected(HealthPack collectedPack)
    {
        _spawnedPackes.Remove(collectedPack);

        collectedPack.PackCollected -= OnPackCollected;
    }
}
