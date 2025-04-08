using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeleeCombat))]
[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(EnemyCombat))]
public class Chasing : MonoBehaviour
{
    private MeleeCombat _meleeCombat;
    private Mover _mover;
    private CharacterAnimator _characterAnimator;
    private EnemyCombat _enemyCombat;
    
    private Character _target;
    private Coroutine _chasingCoroutine;
    private Coroutine _movingCoroutine;

    private float _attackRange = 2f;
    private float _direction;

    public event Action TargetLost;

    private void Awake()
    {
        _meleeCombat = GetComponent<MeleeCombat>();
        _mover = GetComponent<Mover>();
        _characterAnimator = GetComponent<CharacterAnimator>();
        _enemyCombat = GetComponent<EnemyCombat>();

        if (_meleeCombat != null)
            _attackRange = _meleeCombat.AttackRange;
    }

    public void StartChasing(Character character)
    {
        _target = character;

        if (_chasingCoroutine != null)
            StopCoroutine(_chasingCoroutine);

        _chasingCoroutine = StartCoroutine(ChasingRoutine());
    }

    public void StopChasing()
    {
        if (_chasingCoroutine != null)
        {
            StopCoroutine(_chasingCoroutine);
            _chasingCoroutine = null;
        }

        _direction = 0;
        _characterAnimator.UpdateMovement(_direction);
        _mover.Stop();
    }

    private IEnumerator ChasingRoutine()
    {
        while (enabled)
        {
            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);

            yield return _movingCoroutine = StartCoroutine(MoveToTarget());

            yield return _enemyCombat.AttackCoroutine();
        }
    }

    private IEnumerator MoveToTarget()
    {
        while (_target != null && (_target.transform.position - transform.position).sqrMagnitude > _attackRange * _attackRange)
        {
            _direction = Mathf.Sign(_target.transform.position.x - transform.position.x);
            _mover.Move(_direction);
            _characterAnimator.UpdateMovement(_direction);

            if (_target == null)
            {
                TargetLost?.Invoke();
            }

            yield return null;
        }

        _direction = 0;
        _characterAnimator.UpdateMovement(_direction);
        _mover.Stop();
    }
}