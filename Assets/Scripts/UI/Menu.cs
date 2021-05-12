using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected GameObject arrow, menuList, HUD;

    public static bool IsPaused { get; protected set; } = false;
    public static bool IsDead { get; protected set; } = false;
    public static bool HasWon { get; protected set; } = false;
    public static bool OptionsOpen { get; protected set; }
    public static bool MainMenuOpen { get; protected set; }
    public static bool LevelSelectionOpen { get; protected set; }
    public GameObject MenuList { get => menuList; }
    // protected Transform Option { get => menuList.transform.GetChild(index); }
    protected Transform Option { get => options[index]; }

    [SerializeField] GameObject screen;
    protected int index = 0;
    bool navigate = false;
    int lastButtonDown = -1;

    List<Transform> options = new List<Transform>();

    public Action OnNavigating;
    public Action OnSelecting;

    protected virtual void OnEnable()
    {
        options = new List<Transform>();

        IsDead = false;
        HasWon = false;

        if (menuList)
            for (int i = 0; i < menuList.transform.childCount; i++)
            {
                Button button;

                if (menuList.transform.GetChild(i).GetChild(0).TryGetComponent(out button))
                {
                    if (button.interactable)
                        options.Add(menuList.transform.GetChild(i));
                }
                else
                {
                    if (menuList.transform.GetChild(i).GetComponent<Button>().interactable)
                        options.Add(menuList.transform.GetChild(i));
                }

                SelectButton();
            }
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

        // index = index < 0 ? menuList.transform.childCount - 1 : index > menuList.transform.childCount - 1 ? 0 : index;
        if (options != null)
            index = index < 0 ? options.Count - 1 : index > options.Count - 1 ? 0 : index;

        if (navigate)
        {
            SelectButton();
            OnNavigating?.Invoke();
            navigate = false;
        }

        if (Input.GetButtonDown("Submit"))
        {
            Action();
            // OnSelecting?.Invoke();
        }
    }

    void SelectButton()
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
        if (screen)
            screen.SetActive(false);
        if (HUD)
            HUD.SetActive(true);
    }

    // Carga la siguiente escena
    public void NextLevel()
    {
        OnSelecting?.Invoke();
        Resume();
        _SceneManager.LoadScene(1);
    }

    // Reinicia la escena actual
    public void RestartLevel()
    {
        OnSelecting?.Invoke();
        Resume();
        _SceneManager.ResetScene();
        MisionManager.Instance.Resetear();
    }

    // Carga el menú principal
    public void GoHome()
    {
        OnSelecting?.Invoke();
        Resume();
        _SceneManager.LoadScene("Home");
    }


    public void PlayGame()
    {
        Play("Level1");
    }
    public void PlayTutorial()
    {
        Play("Tutorial");
    }

    // Inicia el juego
    public virtual void Play(string sceneName)
    {
        OnSelecting?.Invoke();
        Resume();
        GameManager.Instance.LoadGame();
        _SceneManager.LoadScene(sceneName);
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
        OnSelecting?.Invoke();
        Application.Quit();
    }

}
