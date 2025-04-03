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

    [field: SerializeField] public float AttackDuration { get; private set; } = 3f;
    [field: SerializeField] public float AttackRange { get; private set; } = 2f;

    public void Atack()
    {
        if (Time.time - _lastAttackTime < AttackDuration)
        {
            return;
        }

        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, AttackRange + _correctionAccuracy, _targetLayer);

        foreach (Collider2D target in targets)
        {
            if (target.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }

        _lastAttackTime = Time.time;
    }
}
