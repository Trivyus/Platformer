using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue = 100f;

    private float _currentValue;

    public event Action DamageTaken;
    public event Action Recovered;
    public event Action ValueEnded;

    public float CurrentValue => _currentValue;
    public float MaxValue => _maxValue;

    private void Awake() =>
        _currentValue = _maxValue;

    public void TakeDamage(float damage)
    {
        if(damage <= 0) 
            return;

        _currentValue = Mathf.Clamp(_currentValue - damage, 0, _maxValue);
        DamageTaken?.Invoke();

        if (_currentValue <= 0)
            ValueEnded?.Invoke();
    }

    public void Recover(float health)
    {
        if (health <= 0)
            return;

        _currentValue = Mathf.Clamp(_currentValue + health, 0, _maxValue);
        Recovered?.Invoke();
    }
}
