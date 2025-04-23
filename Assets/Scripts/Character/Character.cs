using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Health))]
public class Character : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private MeleeCombat _meleeCombat;
    [SerializeField] private Vampirism _vampirism;

    private Health _health;
    private bool _isJumping;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.ValueEnded += Die;
        _inputReader.JumpButtonPressed += ActionOnJump;
        _inputReader.AttackButtonPressed += ActionOnAttack;
        _inputReader.AbilityButtonPressed += ActionOnAbilityActive;
    }

    private void OnDisable()
    {
        _health.ValueEnded -= Die;
        _inputReader.JumpButtonPressed -= ActionOnJump;
        _inputReader.AttackButtonPressed -= ActionOnAttack;
        _inputReader.AbilityButtonPressed -= ActionOnAbilityActive;
    }

    private void Update()
    {
        _characterAnimator.UpdateMovement(_inputReader.MoveDirection);
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.MoveDirection);
        _characterAnimator.UpdateGrounded(_groundChecker.IsGrounded);

        if (_isJumping)
        {
            _mover.Jump();
            _isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }

        if (other.TryGetComponent(out HealthPack healthPack))
        {
            _health.Recover(healthPack.HealAmount);
            healthPack.Collect();
        }
    }

    private void ActionOnJump(bool isJumping)
    {
        if (_groundChecker.IsGrounded)
            _isJumping = isJumping;
    }

    private void ActionOnAttack(bool isAttacking)
    {
        if (isAttacking && _groundChecker.IsGrounded)
        {
            _characterAnimator.TriggerAtack();
            _meleeCombat.Attack();
        }
    }

    private void ActionOnAbilityActive()
    {
        _vampirism.ActivateVampirism();
    }

    private void Die() =>
        Destroy(gameObject);
}
