using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint[] _spawnPoints;

    public SpawnPoint GetSpawnPosition()
    {
        List<SpawnPoint> freePoints = new();

        foreach (SpawnPoint point in _spawnPoints)
            if (point.IsFree)
                freePoints.Add(point);

        if (freePoints.Count > 0)
        {
            SpawnPoint freepoint = freePoints[Random.Range(0, freePoints.Count)];
            freepoint.ChangePlaceStatus();
            return freepoint;
        }

        return null;
    }
}
