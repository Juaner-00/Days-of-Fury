using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : Menu
{
    //[SerializeField] GameObject deathScreen;
    public static DeathScreen Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            LoseGame();
        }

        if (IsDead == true && HasWon == false)
            Navigate();
    }

    public void LoseGame()
    {
        Pause();
        IsDead = true;
    }

    public override void Action()
    {
        if (Option.gameObject.name == "Home")
        {
            GoHome();
        }
        else if (Option.gameObject.name == "Restart")
        {
            RestartLevel();
        }
    }
}
