using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuSoundManager : SoundController
{
    Menu[] menus;


    protected override void SetUp(bool child)
    {
        menus = GetComponents<Menu>();
        for(int i = 0; i < menus.Length; i++)
        {
            menus[i].OnNavigating += PlayNavigating;
            menus[i].OnSelecting += PlaySelecting;
        }
    }

    private void PlayNavigating()
    {
        PlayActionByName("Navigating");
    }
    private void PlaySelecting()
    {
        PlayActionByName("Selecting");
    }

    private void OnDisable()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].OnNavigating -= PlayNavigating;
            menus[i].OnSelecting -= PlaySelecting;
        }
    }
}
