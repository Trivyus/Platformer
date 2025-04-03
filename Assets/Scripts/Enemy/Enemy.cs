using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterChecker _checker;
    [SerializeField] private Mover _mover;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chasing _chasing;

    private Character _character;

    private void OnEnable()
    {
        _checker.PlayerFound += OnTargetFound;
        _checker.PlayerLost += OnTargetLost;
        _chasing.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _checker.PlayerFound -= OnTargetFound;
        _checker.PlayerLost -= OnTargetLost;
        _chasing.TargetLost -= OnTargetLost;
        _patrol.StopPatrol();
        _chasing.StopChasing();
    }

    private void Start()
    {
        _patrol.StartPatrol();
    }

    private void OnTargetFound(Character character)
    {
        _character = character;
        _patrol.StopPatrol();
        _chasing.StartChasing(_character);
    }

    private void OnTargetLost()
    {
        _character = null;
        _chasing.StopChasing();
        _patrol.StartPatrol();
    }
}
