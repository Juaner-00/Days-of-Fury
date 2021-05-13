using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickUpBase : MonoBehaviour, IPool
{
    public static Action<Vector3, PickUpType> OnPick;
    public static Action<Vector3> OnDespawn;

    [SerializeField]
    bool dontDespawn;

    [SerializeField]
    PickUpType pType;
    Vector3 inicialPosition;

    bool hasPicked;

    static float despawnTime;
    float time;

    Vector3 pickupSpawn;

    #region Sound

    public Action OnSFX;

    #endregion



    private void OnTriggerEnter(Collider other)
    {
        if (!hasPicked)
            if (other.CompareTag("Player"))
            {
                Pick();
                Despawn();
            }
    }

    private void Start()
    {
        if (dontDespawn)
            hasPicked = false;
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
        OnPick?.Invoke(pickupSpawn, pType);
        OnSFX?.Invoke();
        hasPicked = true;
    }

    // Se llama al inicializar el pickup
    public void Instantiate(Pool poolParent)
    {
        inicialPosition = transform.position;

        ParentPool = poolParent;

        StayOnScene = false;
        hasPicked = true;
    }

    // Se llama cuando el pool obtiene el objeto
    public void Begin(Vector3 position, string tag, Vector3 pos)
    {
        pickupSpawn = pos;
        hasPicked = false;
        transform.position = position;
        time = 0;
    }

    // Se llama cuando se devuelva al pool
    public void End()
    {
        hasPicked = true;
        transform.position = inicialPosition;
        pickupSpawn = Vector3.zero;

        if (dontDespawn)
            Destroy(gameObject);
        else
            ParentPool.PushItem(gameObject);
    }

    private void Update()
    {
        if (!dontDespawn)
            if (!hasPicked)
            {
                time += Time.deltaTime;

                if (time > despawnTime)
                    Despawn();
            }
    }

    public static float DespawnTime { get => despawnTime; set => despawnTime = value; }
    public bool StayOnScene { get; set; }
    public Pool ParentPool { get; set; }
}

public enum PickUpType
{
    Shoot, Score, Speed, Life

}
