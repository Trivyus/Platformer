using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CharacterChecker _checker;
    [SerializeField] private Mover _mover;
    [SerializeField] private Patrol _patrol;
    [SerializeField] private Chasing _chasing;
    [SerializeField] private EnemyCombat _enemyCombat;

    private bool _isChasing = false;
    private bool _isPatroling = false;

    private void OnEnable()
    {
        _checker.PlayerFound += OnTargetFound;
        _checker.PlayerLost += OnTargetLost;

        if (!_isChasing && !_isPatroling)
        {
            _patrol.StartPatrol();
            _isPatroling = true;
        }
    }

    private void OnDisable()
    {
        _chasing.TargetReached -= OnReachedTarget;
        _checker.PlayerFound -= OnTargetFound;
        _checker.PlayerLost -= OnTargetLost;
        _patrol.StopPatrol();
        _chasing.StopChasing();
    }

    private void OnTargetFound(Character character)
    {
        _patrol.StopPatrol();
        _isPatroling = false;
        _chasing.StartChasing(character);
        _isChasing = true;
    }

    private void OnTargetLost()
    {
        _chasing.StopChasing();
        _isChasing = false;
        _patrol.StartPatrol();
        _isPatroling = true;
    }

    private void OnReachedTarget()
    {
        _isChasing = false;
        _chasing.TargetReached -= OnReachedTarget;
        _enemyCombat.MakeAttack();
    }
}
