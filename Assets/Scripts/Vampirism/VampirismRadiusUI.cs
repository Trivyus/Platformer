using UnityEngine;

public class VampirismRadiusUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusSprite;
    [SerializeField] float spriteScale = 0.4f;

    private void Awake()
    {
        _radiusSprite.enabled = false;
    }

    public void UpdateView(float radius)
    {
        transform.localScale = radius * spriteScale * Vector3.one;
    }

    public void ShowRadius()
    {
        _radiusSprite.enabled = true;
    }

    public void HideRadius()
    {
        _radiusSprite.enabled = false;
    }
}
