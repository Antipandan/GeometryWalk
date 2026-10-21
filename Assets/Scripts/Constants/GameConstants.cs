using UnityEngine;

public static class GameConstants
{
    public const float defaultMovementSpeed = 5f;
    public const float defaultUpwardsForce = 100f;
    public const float defaultRotationSpeed = 333f;
    public static readonly Vector2 minVelocity = new Vector2(0, -100);
    public static readonly Vector2 maxVelocity = new Vector2(100, 100);

    public const string defaultBackGroundPath = "Assets/TWD/FreeButtonSet/Textures/other/universal_panel_40.png";
    public const string defaultIconPath = "Assets/TWD/FreeButtonSet/Textures/icons/128x128/repeat.png";
    public const string defaultSliderBackground = "Assets/TWD/FreeButtonSet/Textures/buttons/button_50_inactive.png";
}