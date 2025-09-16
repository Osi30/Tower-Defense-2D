using UnityEngine;

/// <summary>
/// This class just for selectable effect
/// </summary>
public class Selectable : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Color _hoverColor;

    private Color _startColor;

    private void Awake()
    {
        _startColor = _spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        _spriteRenderer.color = _hoverColor;
    }

    private void OnMouseExit()
    {
        _spriteRenderer.color = _startColor;
    }
}
