using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeleeCombat))]
[RequireComponent(typeof(CharacterAnimator))]
public class EnemyCombat : MonoBehaviour
{
    private MeleeCombat _meleeCombat;
    private CharacterAnimator _characterAnimator;

    private WaitForSeconds _waitAfterAttack;
    private WaitForSeconds _waitBeforAttack;
    private float _delayBeforAttack = 1f;

    private void Awake()
    {
        _characterAnimator = GetComponent<CharacterAnimator>();
        _meleeCombat = GetComponent<MeleeCombat>();

        _waitAfterAttack = new(_meleeCombat.AttackDuration);
        _waitBeforAttack = new (_delayBeforAttack);
    }

    public IEnumerator AttackCoroutine()
    {
        yield return _waitBeforAttack;

        _characterAnimator.TriggerAtack();
        _meleeCombat.Attack();

        yield return _waitAfterAttack;
    }
}
