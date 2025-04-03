using System.Collections;
using System.Collections.Generic;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }

        if (other.TryGetComponent(out HealthPack healthPack))
        {
            _health.RecoverHealth(healthPack.HealAmount);
            healthPack.Collect();
        }
    }

    private void Update()
    {
        _characterAnimator.UpdateMovement(_inputReader.MoveDirection);

        if (_inputReader.JumpPressed && _groundChecker.IsGrounded)
            _mover.Jump();

        if (_inputReader.AtackPressed && _groundChecker.IsGrounded)
        {
            _characterAnimator.TriggerAtack();
            _meleeCombat.Atack();
        }
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.MoveDirection);
        _characterAnimator.UpdateGrounded(_groundChecker.IsGrounded);
    }
}
