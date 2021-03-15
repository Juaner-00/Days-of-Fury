using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : Menu
{
    //[SerializeField] GameObject victoryScreen;
    public static VictoryScreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            WinGame();
        }

        if (HasWon == true && IsDead == false)
            Navigate();
    }

    // Se llama al gamar el juego
    public void WinGame()
    {
        Pause();
        HasWon = true;
    }
    
    // Maneja los botones
    public override void Action()
    {
        if (Option.gameObject.name == "Home")
        {
            GoHome();
        }
        else if (Option.gameObject.name == "Next Level")
        {
            NextLevel();
        }
        else if (Option.gameObject.name == "Restart")
        {
            RestartLevel();
        }
    }
}
