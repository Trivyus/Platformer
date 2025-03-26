using UnityEngine;

[RequireComponent(typeof(EnemyPatrolBehavior))]
public class Enemy : MonoBehaviour
{
    private EnemyPatrolBehavior _patrolBehavior;

    private void Awake()
    {
        _patrolBehavior = GetComponent<EnemyPatrolBehavior>();
    }

    private void Start()
    {
        _patrolBehavior.StartPatrol();
    }

    private void OnDisable()
    {
        _patrolBehavior.StopPatrol();
    }
}
