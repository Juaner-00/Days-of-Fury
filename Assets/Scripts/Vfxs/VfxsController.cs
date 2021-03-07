using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxsController : MonoBehaviour
{
    
    [SerializeField] float lifeTime = 10f;

    new Rigidbody rigidbody;
    Vector3 initial;

    string tag;

    public void Instantiate()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        initial = transform.position;
        GetComponent<SphereCollider>().enabled = false;
    }

    public void Begin(Vector3 position, string tag)
    {
        this.tag = tag;

        transform.position = position;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = false;
        Invoke("End", lifeTime);
        GetComponent<SphereCollider>().enabled = true;
    }

    public void End()
    {
        transform.position = initial;
        rigidbody.isKinematic = true;
        GetComponent<SphereCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        IDamagable destructible = collision.gameObject.GetComponent<IDamagable>();
        if (destructible != null)
        {
            if (!collision.gameObject.CompareTag(tag))
            {
                destructible.TakeDamage();
                End();
            }
        }
    }
}
