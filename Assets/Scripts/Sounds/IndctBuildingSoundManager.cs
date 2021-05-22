using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndctBuildingSoundManager : SoundController
{
    protected const float SPACING_3D = 0.75f;

    public void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt", SPACING_3D);
    }

    protected override void SetUp(bool child)
    {
        
    }
}
