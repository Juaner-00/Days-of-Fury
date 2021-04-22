using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTowerSoundManager : SoundController
{
    EnemyTowerController tower;

    protected override void ChildAwake()
    {
        tower = GetComponent<EnemyTowerController>();

        tower.OnShooting += PlayShooting;
        tower.OnGettingHurt += PlayGettingHurt;
    }

    private void PlayShooting() 
    {
        PlaySourceByName("Shooting");
    }

    private void PlayGettingHurt()
    {
        PlaySourceByName("GettingHurt");
    }
}
