using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DestructibleWall : MonoBehaviour, IDamagable
{
   
    public static event WallEvent WallDestroyed;
    public delegate void WallEvent();

    PoolVfxs particleDestruction;

    public void Awake()
    {
       
    }

  

    void Die()
    {

     
        WallDestroyed?.Invoke();

        // Desactivar los renderer de los hijos
        foreach (Renderer render in GetComponentsInChildren<Renderer>())
            render.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);

        // Desactivar los renderer y el collider
        GetComponent<Renderer>().enabled = false;

      
        DynamicGridObstacle dynamic = GetComponentInChildren<DynamicGridObstacle>();

        // Updatear el grid
        dynamic.DoUpdateGraphs();
       
       
    }
}

