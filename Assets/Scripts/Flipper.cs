using UnityEngine;

public class Flipper : MonoBehaviour
{
    private Quaternion _rotationRight;
    private Quaternion _rotationLeft;

    private void Awake()
    {
        _rotationRight = Quaternion.Euler(0, 0, 0);
        _rotationLeft = Quaternion.Euler(0, 180, 0);
    }

    public void Flip(ref bool isFacingRight)
    {
        isFacingRight = !isFacingRight;
        transform.rotation = isFacingRight ? _rotationRight : _rotationLeft;
    }
}