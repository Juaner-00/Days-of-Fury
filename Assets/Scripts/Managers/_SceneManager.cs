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
        string name = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + number).name;
        lsm.LoadScene(name);
    }

    // Se vuelve a cargar la escena actual
    public static void ResetScene()
    {
        lsm.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static _SceneManager Instance { get; private set; }
}
