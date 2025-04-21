using UnityEngine;

public abstract class HealthUIBase : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected virtual void Start()
    {
        Initialize();
        _health.DamageTaken += OnValueChangedUpdate;
        _health.Recovered += OnValueChangedUpdate;
    }

    protected virtual void OnDisable()
    {
        _health.DamageTaken -= OnValueChangedUpdate;
        _health.Recovered -= OnValueChangedUpdate;
    }

    protected abstract void Initialize();
    protected abstract void OnValueChangedUpdate();
}
