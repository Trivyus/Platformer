using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    private readonly int _rotationAngleRight = 0;
    private readonly int _rotationAngleLeft = 180;

    public void Flip(ref bool _isFacingRight)
    {
        _isFacingRight = !_isFacingRight;
        transform.rotation = Quaternion.Euler(0, _isFacingRight ? _rotationAngleRight : _rotationAngleLeft, 0);
    }
}
