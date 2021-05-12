using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DestructibleWall : MonoBehaviour, IDamagable
{
    [SerializeField] int healthPoints = 3;

    public int MaxHealthPoints => 0;

    public int HealthPoints => healthPoints;

    public event WallEvent OnWallDestroyed;
    public static Action OnDestoy;
    public delegate void WallEvent();

    public Action OnDying;
    public Action OnGettingHurt;

    public void TakeDamage()
    {
        healthPoints--;

        // Activar las partículas

        if (healthPoints <= 0)
            Die();

        OnGettingHurt?.Invoke();
    }

    void Die()
    {
        OnWallDestroyed?.Invoke();
        OnDestoy?.Invoke();
        OnDying?.Invoke();

        // // Desactivar los renderer de los hijos
        Renderer[] renders = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < renders.Length - 1; i++)
            renders[i].enabled = false;

        GetComponentInChildren<ParticleSystem>().Play();

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

        // Encender el collider de los escombros
        transform.GetChild(0).gameObject.SetActive(true);
        Collider trigger;

        if (transform.GetChild(0).TryGetComponent(out trigger))
            trigger.enabled = true;
        else
            for (int i = 0; i < transform.GetChild(0).childCount; i++)
                transform.GetChild(0).GetChild(i).GetComponent<Collider>().enabled = true;
    }
}

