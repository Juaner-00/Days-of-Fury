using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSoundManager : SoundController
{
    const float SPACING_3D = 0.75f;

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
        PlayActionByName("Dying", SPACING_3D);
    }
    private void PlayGettingHurt()
    {
        PlayActionByName("GettingHurt", SPACING_3D);
    }

    private void OnDisable()
    {
        SetSubscribeEvents(false);
    }
}
