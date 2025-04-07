using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Health))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private Health _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.DamageTaken += TriggerHurt;
    }

    private void OnDisable()
    {
        _health.DamageTaken -= TriggerHurt;
    }

    public void UpdateMovement(float speed)
    {
        _animator.SetFloat(AnimatorData.Params.SpeedHash, Mathf.Abs(speed));
    }

    public void UpdateGrounded(bool isGrounded)
    {
        _animator.SetBool(AnimatorData.Params.IsGroundedHash, isGrounded);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(AnimatorData.Params.JumpTriggerHash);
    }

    public void TriggerAtack()
    {
        _animator.SetTrigger(AnimatorData.Params.AttackTrigerHash);
    }

    public void TriggerHurt()
    {
        _animator.SetTrigger(AnimatorData.Params.HurtHash);
    }
}
