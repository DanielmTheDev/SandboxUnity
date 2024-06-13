
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsMagnitudeNegligible(this Vector3 vector) => vector.sqrMagnitude <= 0.01f;
        public static bool IsTransformInRange(this Vector3 origin, Vector3 target, float distance) => Vector3.Distance(origin, target) < distance;
    }
}