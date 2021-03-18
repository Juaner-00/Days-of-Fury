using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpeedPickUp : PickUpBase
{
    [SerializeField] float speedPorcentGain;

    protected override void Pick()
    {
        GameManager.Player.GetComponentInChildren<TurretTank>().GainShootSpeed(speedPorcentGain);
        base.Pick();
    }
}
