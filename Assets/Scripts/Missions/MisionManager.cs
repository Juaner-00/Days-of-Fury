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
    [SerializeField] int actualCount;

    int missionObjective;
    int missionsComplets;


    private void Awake()
    {
       if (Instance)
            Destroy(gameObject);
        Instance = this;
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
    }

    void OnScoreGet(int score)
    {
        if (missions[actualMision].opcion == Missions.Opcion.Score)
        {
            actualCount += score;
            IsComplete();
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
