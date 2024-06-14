using UnityEngine;

namespace Extensions
{
    public static class AnimatorExtensions
    {
        public static readonly int HitTrigger = Animator.StringToHash("OnHit");
        public static readonly int SwingTrigger = Animator.StringToHash("Swing");
    }
}