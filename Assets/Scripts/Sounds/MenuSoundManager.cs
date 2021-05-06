using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuSoundManager : SoundController
{
    Menu menu;


    protected override void SetUp(bool child)
    {
        menu = GetComponent<Menu>();

        menu.OnNavigating += PlayNavigating;
        menu.OnSelecting += PlaySelecting;
    }

    private void PlayNavigating()
    {
        PlayActionByName("Navigating");
    }
    private void PlaySelecting()
    {
        PlayActionByName("Selecting");
    }
}
