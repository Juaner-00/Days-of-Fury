using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : Menu
{
    [SerializeField] GameObject levelSelection;

    private void Start()
    {
        MainMenuOpen = true;
        LevelSelectionOpen = false;

        Transform button = MenuList.transform.GetChild(0);
        if (GameManager.Instance.DataObject.PlayedOnce)
        {
            button.name = "LevelSelection";
            button.GetComponentInChildren<TextMeshProUGUI>().text = "LEVEL SELECTION";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(OpenLevelSelection);
        }
        else
        {
            button.name = "Play";
            button.GetComponentInChildren<TextMeshProUGUI>().text = "PLAY";
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.GetComponent<Button>().onClick.AddListener(PlayGame);
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
        levelSelection.SetActive(true);
        MainMenuOpen = false;
        LevelSelectionOpen = true;
    }
}
