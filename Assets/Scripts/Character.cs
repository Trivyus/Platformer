using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover), typeof(Animator))]
public class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Mover _mover;

    private float _moveDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _moveDirection = Input.GetAxis("Horizontal");
        _animator.SetFloat("Speed", Mathf.Abs(_moveDirection));

        if (Input.GetButtonDown("Jump") && _groundChecker.IsGrounded)
        {
            _mover.Jump();
        }
    }

    private void FixedUpdate()
    {
        _mover.Move(_moveDirection);

        _animator.SetBool("IsGrounded", _groundChecker.IsGrounded);
    }
}
