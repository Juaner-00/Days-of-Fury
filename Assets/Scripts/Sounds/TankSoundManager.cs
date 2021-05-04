using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSoundManager : SoundController
{
    private ReticleController reticle;


    protected override void SetUp(bool child)
    {
        reticle = GetComponent<ReticleController>();

        
    }


    private void SetSubscribeEvents(bool setSubscription)
    {
        if (setSubscription)
        {
            reticle.OnShooting += PlayShooting;

            PlayerMovementVels.OnMoving += PlayMoving;
            PlayerMovementVels.OnStoped += StopMoving;

            PlayerHealth.OnGettingHurt += PlayGettingHurt;
            PlayerHealth.OnDie += PlayDying;
        }
        else
        {
            reticle.OnShooting -= PlayShooting;

            PlayerMovementVels.OnMoving -= PlayMoving;
            PlayerMovementVels.OnStoped -= StopMoving;

            PlayerHealth.OnGettingHurt -= PlayGettingHurt;
            PlayerHealth.OnDie -= PlayDying;
        }
    }


    private void PlayShooting()
    {
        PlayActionByName("Shooting");
    }

    private void PlayMoving()
    {
        PlayActionByName("Moving", false, true);
    }

    private void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt");
    }

    private void PlayDying()
    {
        PlayActionByName("Dying");
    }

    private void StopMoving()
    {
        StopActionByName("Moving");
    }
}
