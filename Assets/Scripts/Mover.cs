using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody;
    private bool _isFacingRight = true;
    private int _rotationAngleRight = 0;
    private int _rotationAngleLeft = 180;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveDirection)
    {
        if (moveDirection > 0 && !_isFacingRight)
            Flip();
        else if (moveDirection < 0 && _isFacingRight)
            Flip();

        _rigidbody.velocity = new Vector2(moveDirection * _moveSpeed, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.rotation = Quaternion.Euler(0, _isFacingRight ? _rotationAngleRight : _rotationAngleLeft, 0);
    }
}
