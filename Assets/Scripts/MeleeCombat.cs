using System.Collections;
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

    public void Atack()
    {
        if (Time.time - _lastAttackTime < AttackDuration)
        {
            return;
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AttackRange + _correctionAccuracy, _targetLayer);

        int hitCount = Physics2D.OverlapCircleNonAlloc(transform.position, AttackRange + _correctionAccuracy, _hitBuffer, _targetLayer);

        for (int i = 0; i < hitCount; i++)
        {
            if (_hitBuffer[i].TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }

        _lastAttackTime = Time.time;
    }
}
