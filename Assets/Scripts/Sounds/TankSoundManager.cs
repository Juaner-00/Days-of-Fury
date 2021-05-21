using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSoundManager : SoundController
{
    const float SPACING_3D = 0.4f;

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
        PlayActionByName("Shooting", SPACING_3D /*+ (0.75f - SPACING_3D)*/);
    }

    protected void PlayMoving()
    {
        Debug.Log($"Tank Sound Mng is null = {this == null}");
        PlayActionByName("Moving", SPACING_3D, false, true);
    }

    protected void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt", SPACING_3D);
    }

    protected void PlayDying()
    {
        PlayActionByName("Dying", SPACING_3D);
    }

    protected void StopMoving()
    {
        StopActionByName("Moving");
    }

    private void OnDisable()
    {
        SetSubscribeEvents(false);
    }
}
