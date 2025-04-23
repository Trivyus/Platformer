using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private float _healthPerSecond = 1;
    [SerializeField] private float _range = 5f;
    [SerializeField] private float _abilityDuration = 6f;
    [SerializeField] private float _cooldown = 4f;

    [SerializeField] private LayerMask _targetLayer;

    [SerializeField] private Health _playerHealth;
    [SerializeField] private VampirismRadiusUI _radiusUI;

    private bool _isAbilityActive = false;
    private bool _isOnCooldown = false;

    public event Action<float> AbilityStarted;
    public event Action<float> AbilityOnCooldawn;

    public void ActivateVampirism()
    {
        if (!_isAbilityActive && !_isOnCooldown)
        {
            _isAbilityActive = true;
            _radiusUI.UpdateView(_range);
            _radiusUI.ShowRadius();
            AbilityStarted?.Invoke(_abilityDuration);
            StartCoroutine(VampirismRoutine());
        }
    }

    private IEnumerator VampirismRoutine()
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

        DeactivateVampirism();
    }

    private Health FindClosestEnemy()
    {
        const int maxEnemies = 10;
        Collider2D[] hitColliders = new Collider2D[maxEnemies];
        int numEnemies = Physics2D.OverlapCircleNonAlloc(transform.position, _range, hitColliders, _targetLayer);

        Vector2 playerPos = transform.position;
        Health closestEnemy = null;
        float closestSqrDistance = Mathf.Infinity;

        HashSet<GameObject> processedEnemies = new();

        for (int i = 0; i < numEnemies; i++)
        {
            GameObject enemyRoot = hitColliders[i].transform.root.gameObject;

            if (processedEnemies.Contains(enemyRoot))
                continue;

            processedEnemies.Add(enemyRoot);

            float sqrDistance = (playerPos - (Vector2)hitColliders[i].transform.position).sqrMagnitude;

            if (sqrDistance < closestSqrDistance && hitColliders[i].TryGetComponent<Health>(out var enemyHealth))
            {
                closestSqrDistance = sqrDistance;
                closestEnemy = enemyHealth;
            }
        }

        return closestEnemy;
    }

    private void DeactivateVampirism()
    {
        _isAbilityActive = false;
        _radiusUI.HideRadius();
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        _isOnCooldown = true;
        AbilityOnCooldawn?.Invoke(_cooldown);
        yield return new WaitForSeconds(_cooldown);
        _isOnCooldown = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
