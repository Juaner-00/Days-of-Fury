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

    // Se llama al inicializar el pool
    public void Instantiate()
    {
        initial = transform.position;

        StayOnScene = false;
    }

    // Se llama cuando el pool obtiene el objeto
    public void Begin(Vector3 position, string tag, Vector3 _)
    {
        this.tag = tag;

        transform.position = position;
        thisparticle.Play();
        Invoke("End", lifeTime);
    }

    // Se llama cuando se devuelve el objeto al pool
    public void End()
    {
        thisparticle.Stop();
        transform.position = initial;
    }

    public bool StayOnScene { get; set; }
}
