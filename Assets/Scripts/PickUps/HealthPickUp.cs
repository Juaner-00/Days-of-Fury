using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUpBase
{
    protected override void Pick()
    {
        GameManager.Player.GetComponent<PlayerHealth>().GainLife();
        base.Pick();
    }
}
