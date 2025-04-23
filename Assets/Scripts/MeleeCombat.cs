using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
    private const int MaxTargets = 10;

    [SerializeField] private int _damage = 35;
    [SerializeField] private LayerMask _targetLayer;

    private float _lastAttackTime;
    private float _correctionAccuracy = 1f;

    private Collider2D[] _hitBuffer;

    [field: SerializeField] public float AttackDuration { get; private set; } = 3f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;

    private void Awake()
    {
        _hitBuffer = new Collider2D[MaxTargets];
    }

    public void Attack()
    {
        if (Time.time - _lastAttackTime < AttackDuration)
            return;

        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange + _correctionAccuracy, _hitBuffer, _targetLayer);
        HashSet<Health> damagedTargets = new();

        for (int i = 0; i < hitCount; i++)
            if (_hitBuffer[i].TryGetComponent(out Health health) && damagedTargets.Add(health))
                health.TakeDamage(_damage);

        _lastAttackTime = Time.time;
    }
}
