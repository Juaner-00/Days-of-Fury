using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Michsky.LSS;

public class _SceneManager : MonoBehaviour
{
    public static LoadingScreenManager lsm;


    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;

        lsm = GetComponentInChildren<LoadingScreenManager>();
    }

    // Se carga la escena con el nombre de la escena
    public static void LoadScene(string name)
    {
        lsm.LoadScene(name);
    }

    // Se carga la escena con un desface en el build settings
    public static void LoadScene(int number)
    {
        Scene actualScene = SceneManager.GetActiveScene();

        string newName;

        switch (actualScene.name)
        {
            case "Tutorial":
                newName = "Level1";
                break;
            case "Level1":
                newName = "Level2";
                break;
            case "Level2":
                newName = "Level3";
                break;
            default:
                newName = "Level1";
                break;
        }
        // Scene newScene = SceneManager.GetSceneByBuildIndex(/* actualScene.buildIndex + number */2);
        // string name = .name;
        lsm.LoadScene(newName);
    }

    public void LoadSceneByString(string name)
    {
        lsm.LoadScene(name);
    }

    // Se vuelve a cargar la escena actual
    public static void ResetScene()
    {
        lsm.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static _SceneManager Instance { get; private set; }
    public static bool IsHome => SceneManager.GetActiveScene().name == "Home";
    public static bool IsTutorial => SceneManager.GetActiveScene().name == "Tutorial";
    public static bool IsLevel1 => SceneManager.GetActiveScene().name == "Level1";
    public static bool IsLevel2 => SceneManager.GetActiveScene().name == "Level2";
    public static bool IsLevel3 => SceneManager.GetActiveScene().name == "Level3";
}
