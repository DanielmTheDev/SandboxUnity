
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsMagnitudeNegligible(this Vector3 vector) => vector.sqrMagnitude <= 0.01f;
    }
}