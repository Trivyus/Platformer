using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpButton = "Jump";
    private const string AttackButton = "Fire1";
    private const string AbilityButton = "Ability";

    private bool _wasJumpPressed;
    private bool _wasAttackPressed;
    private bool _wasAbilityPressed;

    public event Action<bool> JumpButtonPressed;
    public event Action<bool> AttackButtonPressed;
    public event Action AbilityButtonPressed;

    public float MoveDirection { get; private set; }

    private void Update()
    {
        MoveDirection = Input.GetAxis(HorizontalAxis);

        bool isJumpPressed = Input.GetButton(JumpButton);

        if (isJumpPressed != _wasJumpPressed)
        {
            JumpButtonPressed?.Invoke(isJumpPressed);
            _wasJumpPressed = isJumpPressed;
        }

        bool isAttackPressed = Input.GetButton(AttackButton);

        if (isAttackPressed != _wasAttackPressed)
        {
            AttackButtonPressed?.Invoke(isAttackPressed);
            _wasAttackPressed = isAttackPressed;
        }

        bool isAbilityPressed = Input.GetButton(AbilityButton);

        if (isAbilityPressed != _wasAbilityPressed)
        {
            AbilityButtonPressed?.Invoke();
            _wasAbilityPressed = isAbilityPressed;
        }
    }
}
