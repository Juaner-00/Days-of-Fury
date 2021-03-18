using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraManager : MonoBehaviour
{
    [SerializeField]
    GameObject cam1, cam2, cam3;
    [SerializeField]
    Vector3 offSet, offSetZoom;
        

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
    public void ZoomCam()
    {
        cam3.transform.position = cam1.transform.position - offSetZoom;
        cam1.SetActive(false);
        cam3.SetActive(true);
    }

    public void EndZoom()
    {
        cam1.transform.position = cam3.transform.position + offSetZoom;
        cam3.SetActive(false);
        cam1.SetActive(true);

    }
    public static CamaraManager Instance { get; private set; }
}
