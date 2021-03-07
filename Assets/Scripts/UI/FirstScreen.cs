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

    private void Update()
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
    }

    public override void Action()
    {

    }
}
