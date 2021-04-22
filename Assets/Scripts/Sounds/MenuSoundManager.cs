using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : SoundController
{
    Menu[] menus;

    protected override void ChildAwake()
    {
        menus = GetComponents<Menu>();

        for (int i = 0; i < menus.Length; i++) { 
            menus[i].OnNavigating += PlayNavigating;
            menus[i].OnSelecting += PlaySelecting;
        }
    }

    void PlayNavigating() {
        PlaySourceByName("Navigating");
    }
    void PlaySelecting()
    {
        PlaySourceByName("Selecting");
    }
}
