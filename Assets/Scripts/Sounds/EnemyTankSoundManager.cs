using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankSoundManager : SoundController
{
    EnemyShootController fov;
    EnemyController controller;

    protected override void ChildAwake()
    {
        fov = GetComponent<EnemyShootController>();
        controller = GetComponent<EnemyController>();

        //fov.OnMoving += PlayMoving;
        fov.OnShooting += PlayShooting;
        controller.OnGettingHurt += PlayGettingHurt;
    }

    private void PlayMoving() {
        PlaySourceByName("Moving", null, true);
    }
    private void PlayShooting()
    {
        PlaySourceByName("Shooting");
    }
    private void PlayGettingHurt()
    {
        PlaySourceByName("GettingHurt");
    }

    private void OnDisable() {
        //fov.OnMoving -= PlayMoving;
        fov.OnShooting -= PlayShooting;
        controller.OnGettingHurt -= PlayGettingHurt;
    }
}
