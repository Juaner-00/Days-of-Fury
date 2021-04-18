using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DestructibleWall : MonoBehaviour, IDamagable
{
    [SerializeField] int healthPoints = 3;

    public int MaxHealthPoints => 0;

    public int HealthPoints => healthPoints;

    public static event WallEvent WallDestroyed;
    public delegate void WallEvent();

    PoolVfxs particleDestruction;

    public void Awake()
    {
        particleDestruction = GameObject.Find("VFXsBuildExplo(Pool)").GetComponent<PoolVfxs>();
    }
    public void TakeDamage()
    {
        healthPoints--;

        // Activar las partículas

        if (healthPoints <= 0)
            Die();
    }

    void Die()
    {
        
        ParticleSystem destruction = particleDestruction.GetItem(transform.position, tag);

        WallDestroyed?.Invoke();

        // Desactivar los renderer de los hijos
        foreach (Renderer render in GetComponentsInChildren<Renderer>())
            render.enabled = false;

        // Desactivar los renderer y el collider
        GetComponent<Renderer>().enabled = false;

        foreach (Collider collider in GetComponents<Collider>())
            collider.enabled = false;

        DynamicGridObstacle dynamic = GetComponentInChildren<DynamicGridObstacle>();

        // Updatear el grid
        dynamic.DoUpdateGraphs();
        // Desactivar el collider
        foreach (Collider collider in dynamic.gameObject.GetComponents<Collider>())
            collider.enabled = false;
    }
}

