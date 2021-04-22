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
    PoolVfxs poolBullet;
    string tag;



    bool collided;

    public void Instantiate()
    {

        poolBullet = GameObject.Find("VFXsBulletCollision(Pool)").GetComponent<PoolVfxs>();
        if (poolBullet == null)
        {
            print("null");
        }
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        initial = transform.position;
        GetComponent<CapsuleCollider>().enabled = false;
        trail.enabled = false;

        StayOnScene = false;
    }

    public void Begin(Vector3 position, string tag, Vector3 _)
    {
        collided = false;
        this.tag = tag;
        trail.enabled = true;
        transform.position = position;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = false;
        Invoke("End", lifeTime);
        GetComponent<CapsuleCollider>().enabled = true;
    }

    public void End()
    {
        trail.enabled = false;
        transform.position = initial;
        rigidbody.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (!collided)
        {
            ParticleSystem bulletCollision = poolBullet.GetItem(collision.GetContact(0).point, tag);
            //bulletCollision.transform.forward = collision.GetContact(0).normal;
            bulletCollision.transform.forward = -rigidbody.velocity;

            bulletCollision.Play();

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

    public bool StayOnScene { get; set; }
}
