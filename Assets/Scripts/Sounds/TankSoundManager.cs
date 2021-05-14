using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSoundManager : SoundController
{
    private ReticleController reticle;


    protected override void SetUp(bool child)
    {
        GetEventComponents();

        SetSubscribeEvents(false);
        SetSubscribeEvents(true);
    }

    protected virtual void GetEventComponents()
    {
        reticle = GetComponent<ReticleController>();
    }

    protected virtual void SetSubscribeEvents(bool setSubscription)
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


    protected void PlayShooting()
    {
        Debug.Log($"Tank Sound Mng is null = {this == null}");
        PlayActionByName("Shooting");
    }

    protected void PlayMoving()
    {
        Debug.Log($"Tank Sound Mng is null = {this == null}");
        PlayActionByName("Moving", false, true);
    }

    protected void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt");
    }

    protected void PlayDying()
    {
        PlayActionByName("Dying");
    }

    protected void StopMoving()
    {
        StopActionByName("Moving");
    }
}
