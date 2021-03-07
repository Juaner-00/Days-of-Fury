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

    public static void LoadScene(string name)
    {
        lsm.LoadScene(name);
    }

    public static void LoadScene(int number)
    {
        string name = SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + number).name;
        lsm.LoadScene(name);
    }

    public static void ResetScene()
    {
        lsm.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static _SceneManager Instance { get; private set; }
}
