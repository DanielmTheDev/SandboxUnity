using UnityEngine;
using static Extensions.AnimatorExtensions;

public class Lightsaber : MonoBehaviour, IAttacker
{
    public Animator animator;

    public void Attack() => animator.SetTrigger(SwingTrigger);
}
