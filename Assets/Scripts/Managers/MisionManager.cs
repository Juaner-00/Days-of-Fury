using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MisionManager : MonoBehaviour
{

    public int actualMision = 1; //Mision actual
    [SerializeField] TextMeshProUGUI misionText; //Texto

    [Header("Mision 1")]
    [SerializeField] int enemymision1;//Contador objetivo (Mision1)
    [SerializeField] string Mision1Text; //Texto de la mision 1 (lo que va antes de los contadores)
    [SerializeField] int scorePoints1;
    private int enemymision1count;//Contador actual (Mision1)

    [Header("Mision 2")]
    [SerializeField] string Mision2Text; //Texto de la mision 2 (lo que va antes de los contadores)

    [Header("Mision 3")]
    [SerializeField] string Mision3Text; //Texto de la mision 3 (lo que va antes de los contadores)

    

    public void Start()
    {
        Texts();
    }

    public void Update()
    {
        
    }

    public void OnEnable()
    {
        EnemyController.Mision += EnemyController_MisionNotification;
    }

    //Notificacion del sistema
    void EnemyController_MisionNotification(string Mision)
    {
        switch (Mision)
        {
            case "Mision1":
                if (actualMision==1) enemymision1count++;
                break;
            case "Mision2":
                //Aumente o haga lo que quiera
                break;

        }

        MisionComplete();
        Texts();
        
    }

    //Verifica si la mision esta completa
    void MisionComplete()
    {
        #region Mision1
        if (enemymision1count >= enemymision1 && actualMision == 1)
        {
            Debug.Log("Mision 1 completada");
            actualMision++;
            misionText.text = "Mision 1 Completada";
            ScoreManager.Instance.Addscore(scorePoints1);
        }
        #endregion    
    }

    void Texts()
    {
        if (actualMision == 1)
            misionText.text = ( Mision1Text + "( " + enemymision1count + " / " + enemymision1 + ")");

    }

}
