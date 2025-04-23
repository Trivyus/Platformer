using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    private const int MaxEnemies = 10;

    [SerializeField] private float _healthPerSecond = 1;
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldown = 4f;

    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private VampirismRadiusUI _radiusUI;

    private bool _isAbilityActive = false;
    private bool _isOnCooldown = false;

    private Collider2D[] _hitColliders;

    public event Action<float> AbilityStarted;
    public event Action<float> AbilityStartedRecharge;

    private void Awake()
    {
        _hitColliders = new Collider2D[MaxEnemies];
    }

    public void Activate()
    {
        if (!_isAbilityActive && !_isOnCooldown)
        {
            _isAbilityActive = true;
            _radiusUI.UpdateView(_range);
            _radiusUI.ShowRadius();
            AbilityStarted?.Invoke(_abilityDuration);
            StartCoroutine(StartPumpingProcess());
        }
    }

    private IEnumerator StartPumpingProcess()
    {
        float timer = 0f;

        while (timer < _abilityDuration)
        {
            timer += Time.deltaTime;

            Health closestEnemy = FindClosestEnemy();

            if (closestEnemy != null)
            {
                float damage = _healthPerSecond * Time.deltaTime;

                closestEnemy.TakeDamage(damage);
                _playerHealth.Recover(damage);
            }

            yield return null;
        }

        Deactivate();
    }

    private Health FindClosestEnemy()
    {
        int numEnemies = Physics2D.OverlapCircleNonAlloc(transform.position, _range, _hitColliders, _targetLayer);

        Vector2 playerPos = transform.position;
        Health closestEnemy = null;
        float closestSqrDistance = Mathf.Infinity;

        HashSet<GameObject> processedEnemies = new();

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject enemyRoot = _hitColliders[i].transform.root.gameObject;

            if (processedEnemies.Contains(enemyRoot))
                continue;

            processedEnemies.Add(enemyRoot);

            float sqrDistance = (playerPos - (Vector2)_hitColliders[i].transform.position).sqrMagnitude;

            if (sqrDistance < closestSqrDistance && _hitColliders[i].TryGetComponent<Health>(out var enemyHealth))
            {
                closestSqrDistance = sqrDistance;
                closestEnemy = enemyHealth;
            }
        }

        return closestEnemy;
    }

    private void Deactivate()
    {
        _isAbilityActive = false;
        _radiusUI.HideRadius();
        StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        _isOnCooldown = true;
        AbilityStartedRecharge?.Invoke(_cooldown);
        yield return new WaitForSeconds(_cooldown);
        _isOnCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
