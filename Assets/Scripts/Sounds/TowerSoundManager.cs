using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSoundManager : TankSoundManager
{
    EnemyTowerController tower;

    protected override void GetEventComponents()
    {
        tower = GetComponent<EnemyTowerController>();
    }

    protected override void SetSubscribeEvents(bool setSubscription)
    {
        if (setSubscription)
        {
            tower.OnGettingHurt += PlayGettingHurt;
            tower.OnShooting += PlayShooting;
        }
        else
        {
            tower.OnGettingHurt -= PlayGettingHurt;
            tower.OnShooting -= PlayShooting;
        }
    }

    private void OnDisable()
    {
        SetSubscribeEvents(false);
    }
}
