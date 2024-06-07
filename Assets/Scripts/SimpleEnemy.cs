using System.Collections;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour, IHittable
{
    public float rotationDuration = 2f;
    public float rotationAngle = 90f;

    public void OnHit()
        => StartCoroutine(RotateOverTime(rotationDuration, rotationAngle));

    private IEnumerator RotateOverTime(float duration, float angle)
    {
        var elapsed = 0f;
        var initialRotation = transform.rotation;
        var targetRotation = initialRotation * Quaternion.Euler(0, 0, angle);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsed / duration);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}