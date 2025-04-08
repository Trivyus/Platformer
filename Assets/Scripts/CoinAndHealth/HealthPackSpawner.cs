using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackSpawner : Spawner
{
    [SerializeField] private HealthPack _healthPackPrefab;
    [SerializeField] private int _maxPackes = 2;

    private Dictionary<HealthPack, SpawnPoint> _spawnedPackes;

    private void Awake()
    {
        _spawnedPackes = new ();
    }

    private void Start()
    {
        SpawnHealthPacks();
    }

    public void SpawnHealthPacks()
    {
        for (int i = 0; i < _maxPackes; i++)
            SpawnHealthPack();
    }

    private void SpawnHealthPack()
    {
        SpawnPoint freePoint = GetSpawnPosition();

        HealthPack newPack = Instantiate(_healthPackPrefab, freePoint.transform.position, Quaternion.identity);
        newPack.Collected += ItemCollected;

        _spawnedPackes.Add(newPack, freePoint);
    }

    private void ItemCollected(Item collectedItem)
    {
        if (collectedItem is HealthPack pack)
        {
            _spawnedPackes[pack].ChangePlaceStatus();
            _spawnedPackes.Remove(pack);
            pack.Collected -= ItemCollected;
        }
    }

    private void OnDisable()
    {
        foreach (var spawnedPack in _spawnedPackes)
            spawnedPack.Key.Collected -= ItemCollected;
    }
}
