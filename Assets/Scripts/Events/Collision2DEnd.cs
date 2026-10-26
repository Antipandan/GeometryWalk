using UnityEngine;

public sealed class Collision2DEnd
{
    private Collision2D collision2D;

    public Collision2DEnd(Collision2D collision2D)
    {
        this.collision2D = collision2D;
    }

    public Collision2D Collision2D
    {
        get =>  collision2D;
    }
}