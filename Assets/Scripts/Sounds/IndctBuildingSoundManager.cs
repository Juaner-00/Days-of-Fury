using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndctBuildingSoundManager : SoundController
{
    const float SPACING_3D = 0.75f;

    protected override void SetUp(bool child)
    {
        
    }

    public void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt", SPACING_3D);
    }

    
}
