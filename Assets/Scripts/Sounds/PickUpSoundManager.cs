using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSoundManager : SoundController
{
    protected override void SetUp(bool child)
    {
        
    }

    private void PlaySFX()
    {
        PlayActionByName("SFX");
    }
}
