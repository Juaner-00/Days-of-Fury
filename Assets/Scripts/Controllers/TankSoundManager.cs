using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSoundManager : SoundController
{
    private PlayerHealth health;
    private PlayerMovement movement;
    private ReticleController reticle;

    protected override void ChildAwake()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
        reticle = GetComponent<ReticleController>();

        //  Estos son los llamados que escucha el scrpit para funcionar con los distintos audios:
        //  Moviéndose
        PlayerMovement.OnMoving += PlayMoving;
        //  Apuntando
        //reticle.OnAiming += PlayAiming;
        //reticle.OnStopAiming += StopAiming;
        //  Disparando
        reticle.OnShooting += PlayShooting;
        //  Recibiendo daño
        PlayerHealth.OnGettingHurt += PlayGettingHurt;
    }

    private void PlayMoving() {
        PlaySourceByName("Moving", null, true);
    }
    private void PlayAiming() {
        PlaySourceByName("Aiming");
    }
    private void PlayShooting() {
        PlaySourceByName("Shooting");
    }
    private void PlayGettingHurt() {
        PlaySourceByName("GettingHurt");
    }

    private void StopAiming() {
        StopSourceByName("Aiming");
    }

    private void OnDisable() {
        PlayerMovement.OnMoving -= PlayMoving;
        reticle.OnShooting -= PlayShooting;
        PlayerHealth.OnGettingHurt -= PlayGettingHurt;
    }
}
