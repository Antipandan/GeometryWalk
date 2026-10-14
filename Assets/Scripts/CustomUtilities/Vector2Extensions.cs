using UnityEngine;

namespace CustomUtility
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// clamps the X and Y value of a Vector2. The min vector establishes the minimum x and y values respectively.
        /// Max vector establishes the maximum x and y values respectively
        /// </summary>
        /// <param name="currentVector">Vector to clamp</param>
        /// <param name="min">minimum x and y values</param>
        /// <param name="max">maximum x and y values</param>
        public static Vector2 Clamp(this Vector2 currentVector, Vector2 min, Vector2 max)
        {
            currentVector.x = Mathf.Clamp(currentVector.x, min.x, max.x);
            currentVector.y = Mathf.Clamp(currentVector.y, min.y, max.y);
            return currentVector;
        }
    }
}