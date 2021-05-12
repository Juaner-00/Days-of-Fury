using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSoundManager : SoundController
{
    PickUpBase pickUp;

    protected override void SetUp(bool child)
    {
        pickUp = GetComponent<PickUpBase>();

        pickUp.OnSFX -= PlaySFX;
        pickUp.OnSFX += PlaySFX;
    }

    private void PlaySFX()
    {
        Debug.Log("Picked up");
        PlayActionByName("SFX");
    }
}
