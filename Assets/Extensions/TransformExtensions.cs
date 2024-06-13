using UnityEngine;

namespace Extensions
{
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