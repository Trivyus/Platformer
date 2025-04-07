using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string s_horizontalAxis = "Horizontal";
    private const string s_jumpButton = "Jump";
    private const string s_attackButton = "Fire1";

    public float MoveDirection { get; private set; }

    public event Action<bool> JumpButtonPressed;
    public event Action<bool> AttackButtonPressed;

    private bool _wasJumpPressed;
    private bool _wasAttackPressed;

    private void Update()
    {
        MoveDirection = Input.GetAxis(s_horizontalAxis);

        bool isJumpPressed = Input.GetButton(s_jumpButton);

        if (isJumpPressed != _wasJumpPressed)
        {
            JumpButtonPressed?.Invoke(isJumpPressed);
            _wasJumpPressed = isJumpPressed;
        }

        bool isAttackPressed = Input.GetButton(s_attackButton);

        if (isAttackPressed != _wasAttackPressed)
        {
            AttackButtonPressed?.Invoke(isAttackPressed);
            _wasAttackPressed = isAttackPressed;
        }
    }
}
