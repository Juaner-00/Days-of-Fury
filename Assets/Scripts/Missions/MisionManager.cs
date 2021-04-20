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
    [SerializeField] int actualMision = 0; //Mision actual
    [SerializeField] Missions[] missions;

    int actualCount;
    int missionObjective;
    int missionsComplets;

    //public GameObject playerPosition;
    public ObjetiveManager[] Objetives;

    private void Awake()
    {
       if (Instance)
            Destroy(gameObject);
        Instance = this;

        Texts();
        //layerPosition.transform.localPosition = gameObject.GetComponent<PlayerMovementVels>().transform.localPosition;
    }
    public void Start()
    {
        actualCount = 0;
        actualMision = 0;
        missionsComplets = 0;
        missionObjective = missions[0].objetive;

        Texts();
    }

    public void Resetear()
    {
        actualCount = 0;
        missionsComplets = 0;
        actualMision = 0;
        missionsComplets = 0;
        missionObjective = missions[0].objetive;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ScoreManager.Instance.Addscore(50);
        }
    }

    public void OnEnable()
    {
        EnemyController.OnDie += OnEnemyDead;
        ScoreManager.OnGetScore += OnScoreGet;       
        PlayerMovementVels.OnMovingObjetive += OnMove;
        ObjetiveManager.OnReachObjetive += OnReachObj;
        EnemyTowerController.OnDie += TowerDead;
    }

    void OnScoreGet(int score)
    {
        if (missions[actualMision].opcion == Missions.Opcion.Score)
        {
            actualCount += score;
            IsComplete();
        }
    }

    void OnMove()
    {        
        if (missions[actualMision].opcion == Missions.Opcion.Move)
        {
            actualCount++;
            IsComplete();
        }
    }

    void OnReachObj(int _Objetive)
    {
        if (missions[actualMision].opcion == Missions.Opcion.Objetive)
        {
            if (_Objetive == missions[actualMision].objetive) {
                actualCount = missions[actualMision].objetive;
                IsComplete();
            }
        }
    }

    void OnEnemyDead(Vector3 _)
    {
        if (missions[actualMision].opcion == Missions.Opcion.Enemys)
        {
            actualCount++;
            print($"Actual Objective {actualCount}/{missionObjective}");
            IsComplete();
        }           
    }

    void TowerDead(Vector3 _)
    {
        if (missions[actualMision].opcion == Missions.Opcion.Tower)
        {
            actualCount++;
            print($"Actual Objective {actualCount}/{missionObjective}");
            IsComplete();
        }
    }



    void IsComplete()
    {
        if (actualCount >= missionObjective)
        {
            if (actualMision < missions.Length-1)
            {
                actualMision++;
                actualCount = 0;
                missionObjective = missions[actualMision].objetive;
            }
            missionsComplets++;

            if (missionsComplets == OneMedalObjective) ScoreManager.Instance.ActiveOneStarMedal();
            if (missionsComplets == TwoMedalObjective) ScoreManager.Instance.ActiveTwoStarMedal();
            if (missionsComplets == ThreeMedalObjective) ScoreManager.Instance.ActiveThreeStarMedal();
        }
        Texts();
    }
    void Texts()
    {
        misionText.text = $"{missions[actualMision].description} ( {actualCount} / {missions[actualMision].objetive} )";
    }
}
