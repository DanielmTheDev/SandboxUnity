using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentsInChildren<IHittable>()
                .Concat(collision.gameObject.GetComponentsInParent<IHittable>())
                .ToList()
                .ForEach(hittable => hittable.OnHit());
        }

        Destroy(gameObject);
    }
}