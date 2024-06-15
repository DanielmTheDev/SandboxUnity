using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static void ExecuteHittableIfAnyTagMatches(this GameObject gameObject, params string[] tags)
        {
            if (tags.Any(gameObject.CompareTag))
                gameObject.GetComponentsInChildren<IHittable>()
                    .Concat(gameObject.GetComponentsInParent<IHittable>())
                    .ToList()
                    .ForEach(hittable => hittable.OnHit());
        }
    }
}