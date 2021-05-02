using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : Menu
{
    [SerializeField] GameObject levelSelectionMenu;
    [SerializeField] LevelSelection levelSelection;
    [SerializeField] MainMenu mainMenu;

    private void Update()
    {
        if (!MainMenuOpen && LevelSelectionOpen)
        {
            Navigate();
        }
    }

    // Maneja los botones
    public override void Action()
    {
        if (Option.gameObject.name == "Back")
        {
            CloseLevelSelection();
        }
        else
        {
            LevelSelectionOpen = false;
            Play(Option.gameObject.name);
        }
    }

    public void CloseLevelSelection()
    {
        OnSelecting?.Invoke();
        levelSelectionMenu.SetActive(false);
        MainMenuOpen = true;
        LevelSelectionOpen = false;
        mainMenu.enabled = true; 
        levelSelection.enabled = false; 
    }
}
