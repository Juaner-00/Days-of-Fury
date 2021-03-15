using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpBase : MonoBehaviour, IPool
{
    public static Action OnPick;
    public static Action OnDespawn;

    Vector3 inicialPosition;

    bool isInGame;

    static float despawnTime;
    float time;

    private void OnTriggerEnter(Collider other)
    {
        if (isInGame)
            if (other.CompareTag("Player"))
            {
                Pick();
                Despawn();
            }
    }

    protected virtual void Despawn()
    {
        if (isInGame)
            OnDespawn?.Invoke();

        time = 0;
        End();
    }

    protected virtual void Pick()
    {
        OnDespawn?.Invoke();
        isInGame = false;
    }

    // Se llama al inicializar el pickup
    public void Instantiate()
    {
        inicialPosition = transform.position;
    }

    // Se llama cuando el pool obtiene el objeto
    public void Begin(Vector3 position, string tag)
    {
        isInGame = true;
        transform.position = position;
    }

    // Se llama cuando se devuelva al pool
    public void End()
    {
        isInGame = false;
        transform.position = inicialPosition;
    }

    private void Update()
    {
        if (isInGame)
            time += Time.deltaTime;

        if (time > despawnTime)
            Despawn();
    }

    public static float DespawnTime { get => despawnTime; set => despawnTime = value; }
}
