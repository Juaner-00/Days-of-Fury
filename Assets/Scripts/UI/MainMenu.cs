using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : Menu
{
    [SerializeField] LevelSelection levelSelection;
    [SerializeField] GameObject levelSelectionMenu;

    private void Start()
    {
        MainMenuOpen = true;
        LevelSelectionOpen = false;
        levelSelection.enabled = false;

        Transform button = MenuList.transform.GetChild(0);
        if (GameManager.Instance.DataObject.PlayedOnce)
        {
            button.name = "LevelSelection";
            button.GetComponentInChildren<TextMeshProUGUI>().text = "LEVEL SELECTION";
            button.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            button.GetComponentInChildren<Button>().onClick.AddListener(OpenLevelSelection);
        }
        else
        {
            button.name = "Play";
            button.GetComponentInChildren<TextMeshProUGUI>().text = "PLAY";
            button.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            button.GetComponentInChildren<Button>().onClick.AddListener(PlayGame);
        }
    }

    private void Update()
    {
        if (MainMenuOpen && !LevelSelectionOpen)
            Navigate();
    }

    // Maneja los botones
    public override void Action()
    {
        if (Option.gameObject.name == "Play")
        {
            MainMenuOpen = false;
            PlayGame();
        }
        else if (Option.gameObject.name == "LevelSelection")
        {
            OpenLevelSelection();
        }
        else if (Option.gameObject.name == "Tutorial")
        {
            MainMenuOpen = false;
            PlayTutorial();
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

    void OpenLevelSelection()
    {
        OnSelecting?.Invoke();
        levelSelectionMenu.SetActive(true);
        levelSelection.enabled = true;
        MainMenuOpen = false;
        LevelSelectionOpen = true;
    }
}
