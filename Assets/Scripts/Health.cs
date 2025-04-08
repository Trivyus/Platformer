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
        _currentValue = Mathf.Clamp(_currentValue - damage, 0, _maxValue);
        DamageTaken?.Invoke();

        if (_currentValue <= 0)
            LifeEnded?.Invoke();
    }

    public void Recover(float health)
    {
        _currentValue = Mathf.Clamp(_currentValue + health, 0, _maxValue);
    }
}
