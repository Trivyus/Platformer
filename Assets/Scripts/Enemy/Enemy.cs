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
    [SerializeField] private CharacterAnimator _characterAnimator;

    private Character _character;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _checker.PlayerFounded += OnTargetFound;
        _checker.PlayerLost += OnTargetLost;
        _chasing.TargetLost += OnTargetLost;
        _health.LifeEnded += Die;
    }

    private void OnDisable()
    {
        _checker.PlayerFounded -= OnTargetFound;
        _checker.PlayerLost -= OnTargetLost;
        _chasing.TargetLost -= OnTargetLost;
        _health.LifeEnded -= Die;
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

    private void Die() =>
        Destroy(gameObject);
}
