using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeedPickUp : PickUpBase
{
    [SerializeField] float speedPorcentGain;

    protected override void Pick()
    {
        GameManager.Player.GetComponent<PlayerMovement>()?.GainSpeed(speedPorcentGain);
        GameManager.Player.GetComponent<PlayerMovementVels>()?.GainSpeed(speedPorcentGain);
        base.Pick();
    }
}
