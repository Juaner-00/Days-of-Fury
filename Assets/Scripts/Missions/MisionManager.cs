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
    #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            ScoreManager.Instance.Addscore(50);
        }
    #endif
    }

    public void OnEnable()
    {
        EnemyController.OnDie += OnEnemyDead;
        ScoreManager.OnGetScore += OnScoreGet;       
        PlayerMovementVels.OnMovingObjetive += OnMove;
        ObjetiveManager.OnReachObjetive += OnReachObj;
        EnemyTowerController.OnDie += TowerDead;
        PickUpBase.OnPick += OnPickUp;
        DestructibleWall.OnDestoy += OnWallDestroy;

        EnemyController.KillNormal += OnEnemyNormalDead;
        EnemyController.KillStrong += OnEnemyStrongDead;
    }

    public void OnDisable()
    {
        EnemyController.OnDie -= OnEnemyDead;
        ScoreManager.OnGetScore -= OnScoreGet;
        PlayerMovementVels.OnMovingObjetive -= OnMove;
        ObjetiveManager.OnReachObjetive -= OnReachObj;
        EnemyTowerController.OnDie -= TowerDead;
        PickUpBase.OnPick -= OnPickUp;
        DestructibleWall.OnDestoy -= OnWallDestroy;

        EnemyController.KillNormal -= OnEnemyNormalDead;
        EnemyController.KillStrong -= OnEnemyStrongDead;
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
            if (_Objetive == missions[actualMision].numObjetive)
            {
                actualCount++;
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

    void OnEnemyStrongDead()
    {
        if (missions[actualMision].opcion == Missions.Opcion.Strong)
        {
            actualCount++;
            //print($"Actual Objective {actualCount}/{missionObjective}");
            IsComplete();
        }
    }

    void OnEnemyNormalDead()
    {
        if (missions[actualMision].opcion == Missions.Opcion.Normal)
        {
            actualCount++;
            //print($"Actual Objective {actualCount}/{missionObjective}");
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

    void OnPickUp(Vector3 _ , PickUpType Type)
    {
        if (missions[actualMision].opcion == Missions.Opcion.PickupAll)
        {
            actualCount++;
            IsComplete();
            
        }

        if (missions[actualMision].opcion == Missions.Opcion.PickUpEsp)
        {
            if (Type == missions[actualMision].TypePickup)
            {
                actualCount++;
                IsComplete();
            }
        }
    }

    void OnWallDestroy()
    {
        if (missions[actualMision].opcion == Missions.Opcion.Walls)
        {
            actualCount++;
            IsComplete();
        }
    }


    //Buscar si la mision esta completa
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
