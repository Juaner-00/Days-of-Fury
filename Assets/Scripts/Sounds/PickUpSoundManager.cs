using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSoundManager : SoundController
{
    PickUpBase pickUp;

    protected override void ChildAwake()
    {
        PickUpBase.OnPick += PlaySFX;
    }

    private void PlaySFX(Vector3 a, PickUpType b)
    {
        PlaySourceByName("SFX", true);
    }
}
