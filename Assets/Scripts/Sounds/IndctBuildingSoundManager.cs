using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndctBuildingSoundManager : SoundController
{
    protected override void SetUp(bool child)
    {
        
    }

    public void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt");
    }

    
}
