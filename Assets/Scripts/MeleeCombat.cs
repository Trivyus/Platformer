using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private int _damage = 35;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Transform _viewPosition;

    private float _lastAttackTime;
    private float _correctionAccuracy = 1f;

    private Collider2D[] _hitBuffer;
    private const int _maxTargets = 10;

    [field: SerializeField] public float AttackDuration { get; private set; } = 3f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;

    private void Awake()
    {
        _hitBuffer = new Collider2D[_maxTargets];
    }

    public void Attack()
    {
        if (Time.time - _lastAttackTime < AttackDuration)
        {
            return;
        }

        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange + _correctionAccuracy, _hitBuffer, _targetLayer);
        HashSet<Health> damagedTargets = new HashSet<Health>();

        for (int i = 0; i < hitCount; i++)
        {
            if (_hitBuffer[i].TryGetComponent(out Health health) && !damagedTargets.Contains(health))
            {
                health.TakeDamage(_damage);
                damagedTargets.Add(health);
            }
        }

        _lastAttackTime = Time.time;
    }
}
