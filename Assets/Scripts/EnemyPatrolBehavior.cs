using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(CharacterAnimator))]
public class EnemyPatrolBehavior : MonoBehaviour
{
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private Transform[] _patrolWaypoints;
    [SerializeField] private Mover _mover;
    [SerializeField] private CharacterAnimator _characterAnimator;

    private float _stoppingDistance = 1f;
    private int _currentPointIndex = 0;

    private Coroutine _patrolCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    public void StartPatrol()
    {
        if (_patrolWaypoints != null && _patrolWaypoints.Length > 1)
            _patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    public void StopPatrol()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }
    }

    private IEnumerator PatrolRoutine()
    {
        WaitForSeconds wait = new(_waitTime);

        while (enabled)
        {
            yield return StartCoroutine(MoveToPoint(_patrolWaypoints[_currentPointIndex].position));

            yield return wait;

            _currentPointIndex = (_currentPointIndex + 1) % _patrolWaypoints.Length;
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPosition)
    {
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);

        while ((targetPosition - transform.position).sqrMagnitude > _stoppingDistance * _stoppingDistance)
        {
            _mover.Move(direction);
            _characterAnimator.UpdateMovement(direction);
            yield return null;
        }

        direction = 0f;
        _characterAnimator.UpdateMovement(direction);
        _mover.Move(direction);
    }
}
