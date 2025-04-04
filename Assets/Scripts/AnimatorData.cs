using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int s_speedHash = Animator.StringToHash("Speed");
        public static readonly int s_isGroundedHash = Animator.StringToHash("IsGrounded");
        public static readonly int s_jumpTriggerHash = Animator.StringToHash("JumpTrigger");
        public static readonly int s_attackTrigerHash = Animator.StringToHash("Atack");
        public static readonly int s_hurtHash = Animator.StringToHash("Hurt");
    }
}
