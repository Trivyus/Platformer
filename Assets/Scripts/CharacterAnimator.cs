using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void UpdateMovement(float speed)
    {
        _animator.SetFloat(AnimatorData.Params.s_speedHash, Mathf.Abs(speed));
    }

    public void UpdateGrounded(bool isGrounded)
    {
        _animator.SetBool(AnimatorData.Params.s_isGroundedHash, isGrounded);
    }

    public void TriggerJump()
    {
        _animator.SetTrigger(AnimatorData.Params.s_jumpTriggerHash);
    }

    public void TriggerAtack()
    {
        _animator.SetTrigger(AnimatorData.Params.s_attackTrigerHash);
    }

    public void TriggerHurt()
    {
        _animator.SetTrigger(AnimatorData.Params.s_hurtHash);
    }
}
