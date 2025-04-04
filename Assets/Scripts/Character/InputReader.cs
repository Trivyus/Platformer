using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string HorizontalAxis = "Horizontal";
    public const string JumpButton = "Jump";
    public const string AtackButton = "Fire1";

    public float MoveDirection { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsAtackPressed { get; private set; }

    private void Update()
    {
        MoveDirection = Input.GetAxis(HorizontalAxis);
        IsJumpPressed = Input.GetButtonDown(JumpButton);
        IsAtackPressed = Input.GetButtonDown(AtackButton);
    }
}
