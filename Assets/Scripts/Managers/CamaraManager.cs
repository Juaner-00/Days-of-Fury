using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    [SerializeField]
    GameObject cam1, cam2;
    [SerializeField]
    Vector3 offSet;
   

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }
    
    // Cambia la posición de la cámara
    public void ChangeCam(Vector3 pos)
    {
        cam2.transform.position = pos + offSet;
        cam1.SetActive(false);
        cam2.SetActive(true);
        Time.timeScale = 0.5f;
    }
    
    public static CamaraManager Instance { get; private set; }
}
