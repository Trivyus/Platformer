using UnityEngine;

[RequireComponent(typeof(CharacterAnimator))]
public class Health : MonoBehaviour
{
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    private void Awake() => _currentHealth = _maxHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _characterAnimator.TriggerHurt();

        if (_currentHealth <= 0) Die();
    }

    public void RecoverHealth(float health)
    {
        _currentHealth += health;

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    private void Die() => Destroy(gameObject);
}
