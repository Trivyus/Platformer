using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [field: SerializeField] public bool IsFree { get; private set; } = true;

    public void ChangePlaceStatus() => IsFree = !IsFree;

}
