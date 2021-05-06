using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSoundManager : SoundController
{
    protected override void SetUp(bool child)
    {
        
    }

    private void PlayDestroying()
    {
        PlayActionByName("Destroying");
    }
    private void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt");
    }
}
