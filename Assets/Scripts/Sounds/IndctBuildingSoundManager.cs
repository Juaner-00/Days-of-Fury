using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndctBuildingSoundManager : SoundController
{
    DestructibleWall wall;

    protected override void SetUp(bool child)
    {
        wall = GetComponent<DestructibleWall>();

        SetSubscribeEvents(false);
        SetSubscribeEvents(true);
    }

    private void SetSubscribeEvents(bool setSubscribe)
    {
        if(setSubscribe)
        {
            wall.OnGettingHurt += PlayGettingHurt;
        }
        else
        {
            wall.OnGettingHurt -= PlayGettingHurt;
        }
    }

    private void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt");
    }
}
