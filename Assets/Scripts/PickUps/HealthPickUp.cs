using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUpBase
{
    protected override void Pick()
    {
        GameManager.Player.GetComponentInParent<PlayerHealth>().GainLife();
        base.Pick();
    }
}
