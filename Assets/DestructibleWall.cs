using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DestructibleWall : MonoBehaviour, IDamagable
{
    public int MaxHealthPoints => 0;

    public int HealthPoints => 0;

    public static event WallEvent WallDestroyed;
    public delegate void WallEvent();

    public void TakeDamage()
    {
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

