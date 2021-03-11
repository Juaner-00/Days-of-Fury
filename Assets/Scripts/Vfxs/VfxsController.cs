using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxsController : MonoBehaviour, IPool
{
    ParticleSystem thisparticle;
    [SerializeField] float lifeTime = 10f;

    new Rigidbody rigidbody;
    Vector3 initial;

    string tag;
    void Start()
    {
        thisparticle = GetComponent<ParticleSystem>();
    }
    public void Instantiate()
    {
        
        initial = transform.position;

    }

    public void Begin(Vector3 position, string tag)
    {
        this.tag = tag;

        transform.position = position;
        thisparticle.Play();
        Invoke("End", lifeTime);

    }

    public void End()
    {
        thisparticle.Stop();
        transform.position = initial;

    }
}
