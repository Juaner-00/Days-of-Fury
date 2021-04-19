using System;
using System.Collections.Generic;
using UnityEngine;

public class FirstScreen : Menu
{
    bool firstClick = false;

    public static Action OnFirstClick;

    private void Start()
    {
        IsPaused = true;
    }

    private void LateUpdate()
    {
        if (Input.anyKeyDown && firstClick == false)
        {
            firstClick = true;
            OnFirstClick?.Invoke();
            Resume();
        }
    }


    // Maneja los botones
    public override void Action()
    {

    }
}
