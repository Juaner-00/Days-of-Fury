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

    // Maneja los botones
    public override void Action()
    {
        if (Option.gameObject.name == "Play")
        {
            PlayGame();
        }
        if (Option.gameObject.name == "Tutorial")
        {
            LoadTutorial();
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

    // Inicia el juego
    public void PlayGame()
    {
        MainMenuOpen = false;
        GameManager.Instance.LoadGame();
        _SceneManager.LoadScene(gameSceneName);
    }

    public void LoadTutorial()
    {
        MainMenuOpen = false;
        GameManager.Instance.LoadGame();
        _SceneManager.LoadScene("Tutorial");
    }
}
