using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] [System.Serializable]
public class ParallaxSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] [Range(0f, 1f)]
    [Tooltip("0 means the gameObject will follow the camera one to one. 1 means it wont follow")]
    private float parallaxMultiplier;

    public float ParallaxMultiplier
    {
        get => parallaxMultiplier;
    }

    public float SpriteLength
    {
        get => spriteRenderer == null || spriteRenderer.sprite == null ? 0f : spriteRenderer.sprite.bounds.size.x;
    }
    
}