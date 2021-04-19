using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScreen : Menu
{
    bool firstClick = false;

    private void Start()
    {
        Invoke("First", 0.1f);
    }

    private void LateUpdate()
    {
        if (Input.anyKeyDown && firstClick == false)
        {
            firstClick = true;
            Resume();
        }
    }

    void First()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }

    // Maneja los botones
    public override void Action()
    {

    }
}
