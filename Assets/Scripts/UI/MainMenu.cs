using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : Menu
{
    [SerializeField] string gameSceneName;
    [SerializeField] GameObject levelSelection;

    private void Start()
    {
        MainMenuOpen = true;
        LevelSelectionOpen = false;
        if (GameManager.Instance.DataObject.PlayedOnce)
        {
            MenuList.transform.GetChild(0).name = "LevelSelection";
            MenuList.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = "LEVEL SELECTION";
        }
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
        else if (Option.gameObject.name == "LevelSelection")
        {
            levelSelection.SetActive(true);
            MainMenuOpen = false;
            LevelSelectionOpen = true;
        }
        else if (Option.gameObject.name == "Tutorial")
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
