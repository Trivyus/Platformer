using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(Health))]
public class Character : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private MeleeCombat _meleeCombat;

    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.LifeEnded += Die;
        _inputReader.JumpButtonPressed += ActionOnJump;
        _inputReader.AttackButtonPressed += ActionOnAttack;
    }

    private void OnDisable()
    {
        _health.LifeEnded -= Die;
        _inputReader.JumpButtonPressed -= ActionOnJump;
        _inputReader.AttackButtonPressed -= ActionOnAttack;
    }

    private void Update()
    {
        _characterAnimator.UpdateMovement(_inputReader.MoveDirection);
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.MoveDirection);
        _characterAnimator.UpdateGrounded(_groundChecker.IsGrounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out ICollectible collectible))
        {
            var visitors = new ICollectibleVisitor[] {new HealingVisitor(_health), new ScoreVisitor()};

            collectible.Collect(new MultiVisitor(visitors));
        }
    }

    private void ActionOnJump(bool isJumping)
    {
        if (isJumping && _groundChecker.IsGrounded)
            _mover.Jump();
    }

    private void ActionOnAttack(bool isAttacking)
    {
        if (isAttacking && _groundChecker.IsGrounded)
        {
            _characterAnimator.TriggerAtack();
            _meleeCombat.Atack();
        }
    }

    private void Die() =>
        Destroy(gameObject);
}
