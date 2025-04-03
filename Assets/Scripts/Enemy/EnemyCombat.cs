using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(MeleeCombat))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnemyCombat : MonoBehaviour
{
    private MeleeCombat _meleeCombat;
    private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _meleeCombat = GetComponent<MeleeCombat>();
    }

    public IEnumerator AttackCoroutine()
    {
        WaitForSeconds wait = new(_meleeCombat.AttackDuration);

        yield return new WaitForSeconds(1f);

        _characterAnimator.TriggerAtack();
        _meleeCombat.Atack();

        yield return wait;
    }
}
