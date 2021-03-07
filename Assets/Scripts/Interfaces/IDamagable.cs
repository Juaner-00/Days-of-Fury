using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int MaxHealthPoints { get; }
    int HealthPoints { get; }

    void TakeDamage();
}
