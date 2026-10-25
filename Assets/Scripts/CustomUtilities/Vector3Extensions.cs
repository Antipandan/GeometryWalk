using UnityEngine;

namespace CustomUtility
{
    public static class Vector3Extensions
    {
        public static Vector3 Divide(this Vector3 rhs, float lhs)
        {
            return new Vector3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
        }
    }
}