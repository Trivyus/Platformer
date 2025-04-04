using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    public event Action OnTakeDamage;
    public event Action OnHealthOver;

    private void Awake() => _currentHealth = _maxHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        OnTakeDamage?.Invoke();

        if (_currentHealth <= 0)
            OnHealthOver?.Invoke();
    }

    public void RecoverHealth(float health)
    {
        _currentHealth += health;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }
}
