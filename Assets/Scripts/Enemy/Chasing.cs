using System;
using System.Collections;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    private Coroutine _chasingCoroutine;
    private MeleeCombat _meleeCombat;
    private Mover _mover;
    private CharacterAnimator _characterAnimator;

    private float _attackRange = 2f;
    private float _direction;

    public event Action TargetReached;

    private void Awake()
    {
        _meleeCombat = GetComponent<MeleeCombat>();
        _mover = GetComponent<Mover>();
        _characterAnimator = GetComponent<CharacterAnimator>();

        if (_meleeCombat != null)
            _attackRange = _meleeCombat.AttackRange;
    }

    public void StartChasing(Character character)
    {
        if (_chasingCoroutine != null)
            StopCoroutine(_chasingCoroutine);

        _chasingCoroutine = StartCoroutine(MoveToTarget(character));
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

    private IEnumerator MoveToTarget(Character character)
    {
        while (enabled && (character.transform.position - transform.position).sqrMagnitude > _attackRange * _attackRange)
        {
            _direction = Mathf.Sign(character.transform.position.x - transform.position.x);

            _mover.Move(_direction);
            _characterAnimator.UpdateMovement(_direction);
            
            yield return null;
        }

        TargetReached?.Invoke();
        StopChasing();
    }

}
