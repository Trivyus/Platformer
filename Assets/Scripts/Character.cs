using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputReader), typeof(CharacterAnimator))]
public class Character : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CharacterAnimator _characterAnimator;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _characterAnimator = GetComponent<CharacterAnimator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            coin.Collect();
        }
    }

    private void Update()
    {
        _characterAnimator.UpdateMovement(_inputReader.MoveDirection);

        if (_inputReader.JumpPressed && _groundChecker.IsGrounded)
            _mover.Jump();
    }

    private void FixedUpdate()
    {
        _mover.Move(_inputReader.MoveDirection);
        _characterAnimator.UpdateGrounded(_groundChecker.IsGrounded);
    }
}
