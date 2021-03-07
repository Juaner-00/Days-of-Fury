using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] GameObject arrow, menuList, HUD;

    public static bool IsPaused { get; protected set; } = false;
    public static bool IsDead { get; protected set; } = false;
    public static bool HasWon { get; protected set; } = false;
    public static bool OptionsOpen { get; protected set; } = false;
    public static bool MainMenuOpen { get; protected set; } = false;
    protected Transform Option { get => menuList.transform.GetChild(index); }
    [SerializeField] GameObject screen;
    protected int index = 0;
    //bool buttonPressed = false;

    public Action OnNavigating;
    public Action OnSelecting;

    void Start()
    {
        ColorButton();
    }

    public void Navigate()
    {
        bool up = Input.GetKeyDown(KeyCode.UpArrow);
        bool down = Input.GetKeyDown(KeyCode.DownArrow);
        bool left = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.RightArrow);

        if (up)
            index--;
        if (down)
            index++;
        if (left)
            index--;
        if (right)
            index++;

        index = index < 0 ? menuList.transform.childCount - 1 : index > menuList.transform.childCount - 1 ? 0 : index;

        if (up || down || left || right)
        {
            ColorButton();
            OnNavigating?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            Action();
            OnSelecting?.Invoke();
        }




        //Arreglar esto-------------------------------------------------------------------------------------------------------------

        //float verticalNavigation = Input.GetAxisRaw("Vertical");
        //float horizontalNavigation = Input.GetAxisRaw("Horizontal");

        ////buttonPressed = Input.anyKey;

        //if (verticalNavigation > 0 /*&& buttonDown == false*/ || horizontalNavigation > 0)
        //    index--;
        //if (verticalNavigation < 0 /*&& buttonDown == false*/ || horizontalNavigation < 0)
        //    index++;

        //index = index < 0 ? menuList.transform.childCount - 1 : index > menuList.transform.childCount - 1 ? 0 : index;

        //if (verticalNavigation > 0 || horizontalNavigation > 0 || verticalNavigation < 0 || horizontalNavigation < 0)
        //    ColorButton();

        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        //    Action();








        //Otra cosa, puro invento------------------------------------------

        //float verticalNavigation = Input.GetAxis("Vertical");
        //float horizontalNavigation = Input.GetAxis("Horizontal");

        //Debug.Log("holis soy un " + verticalNavigation);

        ////buttonPressed = Input.anyKey;

        //if (verticalNavigation == 1 /*&& buttonDown == false*/ || horizontalNavigation == 1)
        //    index--;
        //if (verticalNavigation == -1 /*&& buttonDown == false*/ || horizontalNavigation == -1)
        //    index++;

        //index = index < 0 ? menuList.transform.childCount - 1 : index > menuList.transform.childCount - 1 ? 0 : index;

        //if (verticalNavigation == 1 || horizontalNavigation == 1 || verticalNavigation == -1 || horizontalNavigation == -1)
        //    ColorButton();

        //if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        //    Action();
    }

    void ColorButton()
    {
        arrow.transform.position = Option.position;
    }

    public abstract void Action();

    //--------------------------------- Posibles acciones dentro del menú

    public void Pause()
    {
        Time.timeScale = 0;
        screen.SetActive(true);
        HUD.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        screen.SetActive(false);
        HUD.SetActive(true);
    }

    public void NextLevel()
    {
        Resume();
        _SceneManager.LoadScene(1);
    }

    public void RestartLevel()
    {
        Resume();
        _SceneManager.ResetScene();
    }

    public void GoHome()
    {
        Resume();
        _SceneManager.LoadScene("Home");
    }

    public void OpenOptions()
    {

    }

    public void GoBack()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }






















    //if (index > menuList.transform.childCount - 1)
    //    index = 0;
    //else if (index < 0)
    //    index = menuList.transform.childCount - 1;

    /*index < 0 ? list.transform.childCount - 1 : index > list.transform.childCount - 1 ? 0 : index + 1      /*Mathf.Clamp(index, 0, list.transform.childCount - 1)*/








    //[SerializeField] GameObject pause, options, victory, death, HUD, credits;
    //bool isPaused;

    //private void Awake()
    //{
    //    //HUD.SetActive(true);
    //    //pause.SetActive(false);
    //    //options.SetActive(false);
    //    //victory.SetActive(false);
    //    //death.SetActive(false);

    //    isPaused = false;

    //    Time.timeScale = 1;
    //}

    //private void Update()
    //{
    //    if (isPaused == false)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            pause.SetActive(true);
    //            HUD.SetActive(false);
    //            //Time.timeScale = 0;
    //        }
    //    }
    //    if (isPaused == true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            pause.SetActive(false);
    //            HUD.SetActive(true);
    //            //Time.timeScale = 1;
    //        }
    //    }
    //}

    //public void PlayGame()
    //{
    //    //Ir al primer nivel si es la primera vez, sino abrir pantalla de selección
    //}
    //public void OpenOptions()
    //{
    //    HUD.SetActive(false);
    //    pause.SetActive(false);
    //    options.SetActive(true);
    //    victory.SetActive(false);
    //    death.SetActive(false);
    //    credits.SetActive(false);
    //}
    //public void RollCredits()
    //{
    //    HUD.SetActive(false);
    //    pause.SetActive(false);
    //    options.SetActive(false);
    //    victory.SetActive(false);
    //    death.SetActive(false);
    //    credits.SetActive(true);
    //}
    //public void QuitGame()
    //{
    //    Application.Quit();
    //}
    ////---- menu end
    //public void GoHome()
    //{
    //    //Ir a la pantalla de home
    //}
    //public void ResumeLevel()
    //{
    //    HUD.SetActive(true);
    //    pause.SetActive(false);
    //    options.SetActive(false);
    //    victory.SetActive(false);
    //    death.SetActive(false);
    //    credits.SetActive(false);
    //    Time.timeScale = 1;
    //}
    //public void RestartLevel()
    //{
    //    //Ir a la escena en la que se encontraba antes
    //}
    ////---- win, lose, pause y opciones
    ///





}
