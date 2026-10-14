using UnityEngine;

public static class GameConstants
{
    public const float defaultMovementSpeed = 1f;
    public const float defaultUpwardsForce = 100f;
    public const float defaultRotationSpeed = 333f;
    public static readonly Vector2 minVelocity = new Vector2(0, -100);
    public static readonly Vector2 maxVelocity = new Vector2(100, 100);
}