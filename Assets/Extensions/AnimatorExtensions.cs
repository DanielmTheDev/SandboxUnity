using UnityEngine;

namespace Extensions
{
    public static class AnimatorExtensions
    {
        public static readonly int HitTrigger = Animator.StringToHash("OnHit");
        public static readonly int SwingTrigger = Animator.StringToHash("Swing");
        public static readonly int Block1Trigger = Animator.StringToHash("Block1");
        public static readonly int Block2Trigger = Animator.StringToHash("Block2");

        public static void SetOneOfTriggers(this Animator animator, params int[] triggers)
        {
            var trigger = triggers[Random.Range(0, triggers.Length)];
            animator.SetTrigger(trigger);
        }
    }
}