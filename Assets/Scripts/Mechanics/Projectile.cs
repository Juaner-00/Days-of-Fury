using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IPool
{
    [Header("Turret Properties")]
    [SerializeField] float lifeTime = 10f;
    [SerializeField] TrailRenderer trail;
    new Rigidbody rigidbody;
    Vector3 initial;

    string tag;

    bool collided;

    public void Instantiate()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        initial = transform.position;
        GetComponent<SphereCollider>().enabled = false;
        trail.enabled = false;
    }

    public void Begin(Vector3 position, string tag)
    {
        collided = false;
        this.tag = tag;
        trail.enabled = true;
        transform.position = position;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = false;
        Invoke("End", lifeTime);
        GetComponent<SphereCollider>().enabled = true;
    }

    public void End()
    {
        trail.enabled = false;
        transform.position = initial;
        rigidbody.isKinematic = true;
        GetComponent<SphereCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collided)
        {
            IDamagable destructible = collision.gameObject.GetComponent<IDamagable>();
            if (destructible != null)
            {
                if (!collision.gameObject.CompareTag(tag))
                {
                    collided = true;
                    destructible.TakeDamage();

                }
            }
            if (!collision.gameObject.CompareTag(tag))
                End();
        }
    }
}
