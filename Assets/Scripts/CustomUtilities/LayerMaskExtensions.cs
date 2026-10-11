using System;
using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool HasLayer(this LayerMask mask, int exponent = 0)
    {
        if (!IsValidExponent(exponent)) return false;
        return (mask & (1 << exponent)) != 0; 
    }

    public static int GetMostSignificantBitExponent(this LayerMask mask)
    {
        return (int)MathF.Log10(mask.value);
    }

    private static bool IsValidExponent(int exponent)
    {
        return exponent >= 0 && exponent <= 31;
    }
}