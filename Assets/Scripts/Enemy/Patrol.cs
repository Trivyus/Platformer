using System.Collections;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private Transform[] _patrolWaypoints;

    private Mover _mover;
    private CharacterAnimator _characterAnimator;

    private float _stoppingDistance = 1f;
    private int _currentPointIndex = 0;
    private float _direction;

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

        _direction = 0;
        _characterAnimator.UpdateMovement(_direction);
        _mover.Stop();
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
        _direction = Mathf.Sign(targetPosition.x - transform.position.x);

        while ((targetPosition - transform.position).sqrMagnitude > _stoppingDistance * _stoppingDistance)
        {
            _mover.Move(_direction);
            _characterAnimator.UpdateMovement(_direction);
            yield return null;
        }

        _direction = 0f;
        _characterAnimator.UpdateMovement(_direction);
        _mover.Stop();
    }
}
