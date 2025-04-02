using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private static readonly int SpeedHash = Animator.StringToHash("Speed");
    private static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
    private static readonly int JumpTriggerHash = Animator.StringToHash("JumpTrigger");
    private static readonly int AtackTrigerHash = Animator.StringToHash("Atack");
    private static readonly int HurtHash = Animator.StringToHash("Hurt");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateMovement(float speed)
    {
        _animator.SetFloat(SpeedHash, Mathf.Abs(speed));
    }

    public void UpdateGrounded(bool isGrounded)
    {
        _animator.SetBool(IsGroundedHash, isGrounded);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(JumpTriggerHash);
    }

    public void TriggerAtack()
    {
        _animator.SetTrigger(AtackTrigerHash);
    }

    public void TriggerHurt()
    {
        _animator.SetTrigger(HurtHash);
    }
}
