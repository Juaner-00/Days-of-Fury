using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MisionManager : MonoBehaviour
{       
    public static MisionManager Instance { get; private set; }

    [Header("Textos")]
    [SerializeField] TextMeshProUGUI misionText; //Texto

    [Header("Objetivos")]
    [SerializeField] int OneMedalObjective;
    [SerializeField] int TwoMedalObjective;
    [SerializeField] int ThreeMedalObjective;

    [Header("Missions")]
    public int actualMision = 0; //Mision actual
    public Missions[] missions;
    int ActualM;
    int ObjectiveM;
    int actualMission2;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }
    public void Start()
    {
        ActualM= missions[actualMision].actual;
        ObjectiveM = missions[actualMision].objetive;
        Texts();
    }
    public void Update()
    {
        
    }

    public void OnEnable()
    {
        EnemyController.Mission += EnemyController_MisionNotification;
    }

    //Notificacion del sistema
    void EnemyController_MisionNotification(string a, int b)
    {
        switch (a)
        {
            case "Enemy":
                ActualM += b;
                break;

            case "Score":
                ActualM += b;
                break;

            case "Pickups":
                ActualM += b ;
                break;

            case "Walls":
                ActualM += b;
                break;
        }

        if (ActualM >= ObjectiveM)
        {
            if (actualMision < missions.Length-1)
            {
                actualMision++;
                ActualM = missions[actualMision].actual;
                ObjectiveM = missions[actualMision].objetive;
            }            
            actualMission2++;
            if (actualMision == OneMedalObjective) ScoreManager.Instance.ActiveOneStarMedal();
            if (actualMision == TwoMedalObjective) ScoreManager.Instance.ActiveTwoStarMedal();
            if (actualMission2 == ThreeMedalObjective) ScoreManager.Instance.ActiveThreeStarMedal();
        }
        Texts();        
    }

    void Texts()
    {
        misionText.text = $"{missions[actualMision].description} ( {ActualM} / {missions[actualMision].objetive} )";
    }

}
