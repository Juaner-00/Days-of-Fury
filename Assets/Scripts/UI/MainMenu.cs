using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    [SerializeField] string gameSceneName;

    private void Start()
    {
        MainMenuOpen = true;
    }

    private void Update()
    {
        if (MainMenuOpen)
            Navigate();
    }

    public override void Action()
    {
        if (Option.gameObject.name == "Play")
        {
            PlayGame();
        }
        else if (Option.gameObject.name == "Options")
        {
            //Options
        }
        else if (Option.gameObject.name == "Credits")
        {
            //Credits
        }
        else if (Option.gameObject.name == "Quit")
        {
            QuitGame();
        }
    }

    public void PlayGame()
    {
        MainMenuOpen = false;
        _SceneManager.LoadScene(gameSceneName);
    }
}
