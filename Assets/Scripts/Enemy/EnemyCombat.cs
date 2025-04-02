using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeleeCombat))]
public class EnemyCombat : MonoBehaviour
{
    private MeleeCombat _meleeCombat;
    private CharacterAnimator _characterAnimator;

    private bool _isAttacking = false;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _meleeCombat = GetComponent<MeleeCombat>();
    }

    public void MakeAttack()
    {
        if (!_isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    private IEnumerator AttackCoroutine()
    {
        WaitForSeconds wait = new(_meleeCombat.AttackDuration);

        _isAttacking = true;

        yield return new WaitForSeconds(1f);

        _characterAnimator.TriggerAtack();
        _meleeCombat.Atack();

        yield return wait;

        _isAttacking = false;
    }
}
