using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruibleWall : MonoBehaviour, IDamagable
{
    public int MaxHealthPoints => 0;

    public int HealthPoints => 0;

    public void TakeDamage()
    {
        Destroy(gameObject);
    }
}

