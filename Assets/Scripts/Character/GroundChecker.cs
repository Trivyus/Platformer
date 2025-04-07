using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Collider2D _groundTrigger;

    private int _groundCollidersCount;

    public bool IsGrounded { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _groundLayer) != 0)
        {
            _groundCollidersCount++;
            UpdateGroundedState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & _groundLayer) != 0)
        {
            _groundCollidersCount--;
            UpdateGroundedState();
        }
    }

    private void UpdateGroundedState()
    {
        IsGrounded = _groundCollidersCount > 0;
    }
}
