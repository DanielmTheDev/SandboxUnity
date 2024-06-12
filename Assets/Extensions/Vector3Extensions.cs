
using UnityEngine;

namespace Extensions
{
    public static class Vector3Extensions
    {
        public static bool IsMagnitudeNegligible(this Vector3 vector) => vector.sqrMagnitude <= 0.01f;
        public static bool IsTransformInRange(this Vector3 origin, Vector3 target, float distance) => Vector3.Distance(origin, target) < distance;
    }

    public static class TransformExtensions
    {
        public static bool IsInLineOfSight(this Transform source, Transform target, float viewAngle = 90f, string tag = "Player")
        {
            var isPlayerInFront = Vector3.Angle(source.forward, target.position - source.position) < viewAngle;
            var hasHitSomething = Physics.Linecast(source.position, target.position, out var hit);
            var isVisionToPlayerClear = hasHitSomething && hit.collider.CompareTag(tag);
            return isPlayerInFront && isVisionToPlayerClear;
        }
    }
}