using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 100f;

    private float _currentValue;

    public event Action DamageTaken;
    public event Action LifeEnded;

    private void Awake() => _currentValue = _maxValue;

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _currentValue -= damage;
            DamageTaken?.Invoke();
        }

        if (_currentValue <= 0)
            LifeEnded?.Invoke();
    }

    public void Recover(float health)
    {
        if (health > 0)
            _currentValue += health;

        if (_currentValue > _maxValue)
            _currentValue = _maxValue;
    }
}
