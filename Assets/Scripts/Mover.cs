using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Flipper))]
public class Mover : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody;
    private Flipper _flipper;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _flipper = GetComponent<Flipper>();
    }

    public void Move(float moveDirection)
    {
        if (moveDirection > 0 && !_isFacingRight)
            _flipper.Flip(ref _isFacingRight);
        else if (moveDirection < 0 && _isFacingRight)
            _flipper.Flip(ref _isFacingRight);

        _rigidbody.velocity = new Vector2(moveDirection * _moveSpeed, _rigidbody.velocity.y);
    }

    public void Stop()
    {
        _rigidbody.velocity = new Vector2(0 * _moveSpeed, _rigidbody.velocity.y);
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}
