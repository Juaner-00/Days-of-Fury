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
        Destroy(gameObject);
    }
}

