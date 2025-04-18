using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(CharacterAnimator))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private Transform[] _patrolWaypoints;

    private Mover _mover;
    private CharacterAnimator _characterAnimator;

    private float _stoppingDistance = 1f;
    private float _direction;
    private int _currentPointIndex = 0;
    private WaitForSeconds _wait;

    private Coroutine _patrolCoroutine;
    private Coroutine _movingCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _characterAnimator = GetComponent<CharacterAnimator>();

        _wait = new(_waitTime);
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
            StopCoroutine(_movingCoroutine);
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }

        _direction = 0;
        _characterAnimator.UpdateMovement(_direction);
        _mover.Stop();
    }

    private IEnumerator PatrolRoutine()
    {
        while (enabled)
        {
            yield return _movingCoroutine = StartCoroutine(MoveToPoint(_patrolWaypoints[_currentPointIndex].position));

            yield return _wait;

            _currentPointIndex = ++_currentPointIndex % _patrolWaypoints.Length;
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
