using System.Linq;
using Extensions;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 2f;

    public GameObject sparksEffect;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.ExecuteHittableIfAnyTagMatches("Enemy", "Player");
        InstantiateSparksEffect(collision);
        Destroy(gameObject);
    }

    private void InstantiateSparksEffect(Collision collision)
    {
        var effect = Instantiate(sparksEffect, collision.contacts.First().point, Quaternion.identity);
        Destroy(effect, 1f);
    }
}