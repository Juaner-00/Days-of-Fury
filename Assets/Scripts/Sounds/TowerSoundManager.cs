using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSoundManager : SoundController
{
    protected override void SetUp(bool child)
    {
        
    }

    private void PlayShooting()
    {
        PlayActionByName("Shooting");
    }
}
