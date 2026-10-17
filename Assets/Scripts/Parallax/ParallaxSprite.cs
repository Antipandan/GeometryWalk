using System;
using CustomUtility;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))] [System.Serializable]
public class ParallaxSprite : MonoBehaviour
{
    [SerializeField] private Camera cameraTransform;
    [SerializeField] private Transform[] tiles;
    [Range(0f, 1f)]
    [SerializeField] private float parallaxFactor = 0.3f;
    private float spriteWidth;
    private float previousCameraX;

    private void Awake()
    {
        CheckReferences();
    }

    private void CheckReferences()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main;
            previousCameraX = cameraTransform!.transform.position.x;
        }
        if (tiles.Length == 0) Logging.LogEmptyCollectionError(nameof(tiles), ErrorSeverity.Error, gameObject);
        else
        {
            SpriteRenderer tileRenderer = tiles[0].GetComponent<SpriteRenderer>();
            spriteWidth = tileRenderer == null ? 0f :  tiles[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    private void Update()
    {
        // everything assumes we are moving to the right!
        float cameraMovement = cameraTransform!.transform.position.x - previousCameraX;
        previousCameraX = cameraTransform!.transform.position.x;
        float parallaxMovement = cameraMovement * parallaxFactor;
        UpdateTilePosition(parallaxMovement);
        
        Transform leftmost = tiles[0];
        Transform rightmost = tiles[0];

        for (int i = 0; i < tiles.Length; i++)
        {
            Transform currentTile = tiles[i];
            if (currentTile.position.x < leftmost.position.x) leftmost = currentTile;
            if (currentTile.position.x > rightmost.position.x) rightmost = currentTile;
        }

        if (leftmost.position.x + spriteWidth < cameraTransform!.transform.position.x)
        {
            leftmost.position = new Vector3(
                rightmost.position.x + spriteWidth,
                leftmost.position.y,
                leftmost.position.z
            );
        }
    }

    private void UpdateTilePosition(float parallaxMovement)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            Transform currentTile = tiles[i];
            currentTile.position += Vector3.right * parallaxMovement;
        }
    }
}