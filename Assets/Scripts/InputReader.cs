using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string HorizontalAxis = "Horizontal";
    public const string JumpButton = "Jump";

    public float MoveDirection { get; private set; }
    public bool JumpPressed { get; private set; }

    private void Update()
    {
        MoveDirection = Input.GetAxis(HorizontalAxis);
        JumpPressed = Input.GetButtonDown(JumpButton);
    }
}
