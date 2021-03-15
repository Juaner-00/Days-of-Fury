using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : Menu
{
    private void Update()
    {
        if (IsPaused == false && IsDead == false && HasWon == false && OptionsOpen == true)
        {
            Navigate();
        }
    }

    // Maneja los botones
    public override void Action()
    {
        if (Option.gameObject.name == "Back")
        {
            Resume();
            OptionsOpen = false;
        }
        else if (Option.gameObject.name == "Home")
        {
            _SceneManager.LoadScene("Home");
            OptionsOpen = false;
        }
    }
}
