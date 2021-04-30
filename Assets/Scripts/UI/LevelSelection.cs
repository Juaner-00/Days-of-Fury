using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : Menu
{
    private void Update()
    {
        if (MainMenuOpen = false && LevelSelectionOpen == true)
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
            MainMenuOpen = true;
            LevelSelectionOpen = false;
        }
        else 
        {
            Debug.Log("Action Else");
            Resume();
            _SceneManager.LoadScene(Option.gameObject.name);
            LevelSelectionOpen = false;
        }
    }
}
