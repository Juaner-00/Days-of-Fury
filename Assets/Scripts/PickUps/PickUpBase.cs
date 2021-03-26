using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpBase : MonoBehaviour, IPool
{
    public static Action<Vector3,PickUpType> OnPick;
    public static Action<Vector3> OnDespawn;
    [SerializeField]
    PickUpType pType;
    Vector3 inicialPosition;

    bool hasPicked;

    static float despawnTime;
    float time;

    Vector3 pickupSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPicked)
            if (other.CompareTag("Player"))
            {
                Pick();
                Despawn();
            }
    }

    protected virtual void Despawn()
    {
        if (!hasPicked)
            OnDespawn?.Invoke(pickupSpawn);

        time = 0;
        End();
    }

    protected virtual void Pick()
    {
        OnPick?.Invoke(pickupSpawn,pType);
        hasPicked = true;
    }

    // Se llama al inicializar el pickup
    public void Instantiate()
    {
        inicialPosition = transform.position;
    }

    // Se llama cuando el pool obtiene el objeto
    public void Begin(Vector3 position, string tag, Vector3 pos)
    {
        pickupSpawn = pos;
        hasPicked = false;
        transform.position = position;
    }

    // Se llama cuando se devuelva al pool
    public void End()
    {
        hasPicked = true;
        transform.position = inicialPosition;
        pickupSpawn = Vector3.zero;
    }

    private void Update()
    {
        if (hasPicked)
            time += Time.deltaTime;

        if (time > despawnTime)
            Despawn();
    }

    public static float DespawnTime { get => despawnTime; set => despawnTime = value; }
    
}

public enum PickUpType
{
    Shoot, Score, Speed,  Life

}
