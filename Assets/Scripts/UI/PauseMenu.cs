using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    [SerializeField] GameObject /*pauseMenu, */optionsMenu;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (IsPaused == true && IsDead == false && HasWon == false && OptionsOpen == false)
            {
                Resume();
                IsPaused = false;
            }
            else if (IsPaused == false && IsDead == false && HasWon == false && OptionsOpen == false)
            {
                Pause();
                IsPaused = true;
            }
        }

        if (IsPaused == true && IsDead == false && HasWon == false && OptionsOpen == false)
            Navigate();
    }

    public override void Action()
    {

        if (Option.gameObject.name == "Resume")
        {
            Resume();
        }
        else if (Option.gameObject.name == "Restart")
        {
            RestartLevel();
        }
        //else if (Option.gameObject.name == "Options")
        //{
        //    Resume(pauseMenu);
        //    IsPaused = false;
        //    OptionsOpen = true;
        //    Pause(optionsMenu);
        //}
        else if (Option.gameObject.name == "Home")
        {
            GoHome();
        }
    }
}
