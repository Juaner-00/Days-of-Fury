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
    bool navigate = false;
    int lastButtonDown = -1;

    public Action OnNavigating;
    public Action OnSelecting;

    void Start()
    {
        IsDead = false;
        HasWon = false;

        ColorButton();
    }

    // Maneja el movimiento del cursor selector de botones
    public void Navigate()
    {
        float vertival = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (vertival < 0.2f && vertival > -0.2f && horizontal < 0.2f && horizontal > -0.2f)
        {
            lastButtonDown = -1;
            navigate = false;
        }
        if (vertival > 0.2f && lastButtonDown != 0)
        {
            index--;
            lastButtonDown = 0;
            navigate = true;
        }
        if (vertival < -0.2f && lastButtonDown != 1)
        {
            index++;
            lastButtonDown = 1;
            navigate = true;
        }
        if (horizontal > 0.2f && lastButtonDown != 2)
        {
            index++;
            lastButtonDown = 2;
            navigate = true;
        }
        if (horizontal < -0.2f && lastButtonDown != 3)
        {
            index--;
            lastButtonDown = 3;
            navigate = true;
        }


        index = index < 0 ? menuList.transform.childCount - 1 : index > menuList.transform.childCount - 1 ? 0 : index;

        if (navigate)
        {
            ColorButton();
            OnNavigating?.Invoke();
            navigate = false;
        }

        if (Input.GetButtonDown("Submit"))
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
    // Pausa el juego y muestra el panel de pausa
    public void Pause()
    {
        Time.timeScale = 0;
        screen.SetActive(true);
        HUD.SetActive(false);
    }

    // Resume el juego
    public void Resume()
    {
        IsPaused = false;
        Time.timeScale = 1;
        screen.SetActive(false);
        HUD.SetActive(true);
    }

    // Carga la siguiente escena
    public void NextLevel()
    {
        Resume();
        _SceneManager.LoadScene(1);
    }

    // Reinicia la escena actual
    public void RestartLevel()
    {
        Resume();
        _SceneManager.ResetScene();
        MisionManager.Instance.Resetear();
    }

    // Carga el menú principal
    public void GoHome()
    {
        Resume();
        _SceneManager.LoadScene("Home");
    }

    // Abre el menú de opciones
    public void OpenOptions()
    {

    }

    // Abre el panel anterior
    public void GoBack()
    {

    }

    // Cierra el juego
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
