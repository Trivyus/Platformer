using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimatorData
{
    public static class Params
    {
        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");
        public static readonly int JumpTriggerHash = Animator.StringToHash("JumpTrigger");
        public static readonly int AttackTrigerHash = Animator.StringToHash("Atack");
        public static readonly int HurtHash = Animator.StringToHash("Hurt");
    }
}
