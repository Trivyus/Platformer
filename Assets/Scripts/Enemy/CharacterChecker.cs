using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChecker : MonoBehaviour
{
    [SerializeField] private float _viewRadius = 5f;
    [SerializeField] private Color _viewColor = Color.red;
    [SerializeField] private Transform _viewPosition;
    [SerializeField] private LayerMask _targetMask;

    bool _playerFound = false;

    public event Action<Character> PlayerFound;
    public event Action PlayerLost;

    private void Update()
    {
        CanSeePlayer();
    }

    private void CanSeePlayer()
    {
        Vector2 baseDirection = transform.right * Mathf.Sign(-transform.localScale.x);
        Debug.DrawRay(_viewPosition.position, baseDirection * _viewRadius, _viewColor);

        RaycastHit2D hit = Physics2D.Raycast(_viewPosition.position, baseDirection, _viewRadius, _targetMask);

        if (_playerFound == false && hit.collider != null && hit.collider.TryGetComponent(out Character character))
        {
            _playerFound = true;
            PlayerFound?.Invoke(character);
        }
        else if (_playerFound == true && hit.collider == null)
        {
            _playerFound = false;
            PlayerLost?.Invoke();
        }
    }
}
