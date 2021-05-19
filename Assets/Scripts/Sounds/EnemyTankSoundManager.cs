using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSoundManager : TankSoundManager
{
    EnemyStateMachine stateMachine;
    EnemyController enemyController;

    protected override void GetEventComponents()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        enemyController = GetComponent<EnemyController>();
    }
    
    protected override void SetSubscribeEvents(bool setSubscription)
    {
        if (setSubscription)
        {
            stateMachine.OnShooting += PlayShooting;
            stateMachine.OnMoving += PlayMoving;
            enemyController.OnGettingHurt += PlayGettingHurt;
        }
        else
        {
            stateMachine.OnShooting -= PlayShooting;
            stateMachine.OnMoving -= PlayMoving;
            enemyController.OnGettingHurt -= PlayGettingHurt;
        }
    }

    private void OnDisable()
    {
        SetSubscribeEvents(false);
    }
}
