using UnityEngine;
using System;
using System.Collections;
using UnityEditor;
using Object = UnityEngine.Object;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class Ground : MonoBehaviour
{
    [SerializeField] private BoxCollider2D groundCollider;
    [SerializeField] private SpriteRenderer groundSprite;
    [SerializeField] private float length;
    private static float validateDelay = 0f;
    
    private void OnValidate()
    {
        #if UNITY_EDITOR
        // janky way of slowing down OnValidate. Maybe use customEditor?
        validateDelay = (float)EditorApplication.timeSinceStartup + 0.05f;

        EditorApplication.update -= ValidateEditorData;
        EditorApplication.update += ValidateEditorData;
        #endif
    }
    
    public static Ground InstantiateGround()
    {
        return Object.Instantiate(new GameObject()).AddComponent<Ground>();
    }

    private Vector2 ChangeSizeCorrectly(Vector2 currentSize)
    {
        currentSize.x = length;
        return new Vector2(currentSize.x, currentSize.y);
    }

    private void ValidateEditorData()
    {
#if UNITY_EDITOR
        if (EditorApplication.timeSinceStartup < validateDelay) return;
        EditorApplication.update -= ValidateEditorData;
        Validate();
#endif        
        void Validate()
        {
            if (groundCollider != null && groundSprite != null)
            {
                groundCollider.size = ChangeSizeCorrectly(groundCollider.size);
                groundSprite.size = ChangeSizeCorrectly(groundSprite.size);
            }
        }
    }
}