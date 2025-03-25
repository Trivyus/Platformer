using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Mover), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _waitTime = 2f;
    [SerializeField] private Transform[] _patrolWaypoints;
    [SerializeField] private Mover _mover;
    [SerializeField] private Animator _animator;

    private float _stoppingDistance = 1f;
    private int _currentPointIndex = 0;

    private Coroutine _patrolCoroutine;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (_patrolWaypoints != null && _patrolWaypoints.Length > 1)
            _patrolCoroutine = StartCoroutine(PatrolRoutine());
    }

    private IEnumerator PatrolRoutine()
    {
        while (enabled)
        {
            yield return StartCoroutine(MoveToPoint(_patrolWaypoints[_currentPointIndex].position));

            yield return new WaitForSeconds(_waitTime);

            _currentPointIndex = (_currentPointIndex + 1) % _patrolWaypoints.Length;
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPosition)
    {
        float direction = Mathf.Sign(targetPosition.x - transform.position.x);

        while ((targetPosition - transform.position).sqrMagnitude > _stoppingDistance * _stoppingDistance)
        {
            _mover.Move(direction);
            _animator.SetFloat("Speed", Mathf.Abs(direction));
            yield return null;
        }

        direction = 0f;
        _animator.SetFloat("Speed", Mathf.Abs(direction));
        _mover.Move(direction);
    }

    private void OnDisable()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }
    }
}
