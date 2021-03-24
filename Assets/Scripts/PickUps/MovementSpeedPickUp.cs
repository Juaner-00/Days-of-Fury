using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPickUp : PickUpBase
{
    [SerializeField] float speedPorcentGain;

    protected override void Pick()
    {
        GameManager.Player.GetComponentInParent<PlayerMovement>()?.GainSpeed(speedPorcentGain);
        GameManager.Player.GetComponentInParent<PlayerMovementVels>()?.GainSpeed(speedPorcentGain);
        base.Pick();
    }
}
