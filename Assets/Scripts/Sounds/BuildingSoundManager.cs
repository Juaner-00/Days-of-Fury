using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSoundManager : SoundController
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
            wall.OnDying += PlayDying;
        }
        else
        {
            wall.OnGettingHurt -= PlayGettingHurt;
            wall.OnDying -= PlayDying;
        }
    }

    private void PlayDying()
    {
        PlayActionByName("Dying");
    }
    private void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt");
    }

    private void OnDisable()
    {
        SetSubscribeEvents(false);
    }
}
