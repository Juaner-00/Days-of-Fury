using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Expandable]
    [SerializeField] DataObject dataObject;
    [SerializeField] int actualLevel;

    [SerializeField] bool spawnEnemies;
    [SerializeField] bool spawnPickUps;


    bool hasFinished;
    Medals playerMedals;

    static GameObject player;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;

        HasFinished = false;

        if (_SceneManager.IsHome)
            LoadGame();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        playerMedals = Medals.None;
        if (_SceneManager.IsLevel1)
            dataObject.SetPlayed();
    }

    void StartSpawn()
    {
        if (spawnEnemies)
            if (EnemySpawnManager.Instance)
                EnemySpawnManager.Instance.StartSpawning();

        if (spawnPickUps)
            if (PickUpSpawnManager.Instance)
                PickUpSpawnManager.Instance.StartSpawning();
    }

    private void OnEnable()
    {
        PlayerHealth.OnDie += PlayerDie;
        ScoreManager.OnMedalObtained += VictoryCheck;
        FirstScreen.OnFirstClick += StartSpawn;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDie -= PlayerDie;
        ScoreManager.OnMedalObtained -= VictoryCheck;
        FirstScreen.OnFirstClick -= StartSpawn;
    }

    // Método para finalizar el juego
    public void FinishGame()
    {
        if (!_SceneManager.IsTutorial)
            SaveGame();

        if (player)
        {
            player.GetComponentInParent<PlayerMovementVels>().enabled = false;
            player.GetComponentInParent<ReticleController>().enabled = false;

            if (CamaraManager.Instance)
                CamaraManager.Instance.ChangeCam(player.transform.position);
        }

        if (EnemySpawnManager.Instance)
            EnemySpawnManager.Instance.StopSpawn();

        if (PickUpSpawnManager.Instance)
            PickUpSpawnManager.Instance.StopSpawn();

        HasFinished = true;
    }

    // Método que se cuando el jugador muere
    void PlayerDie()
    {
        if (!Menu.HasWon && !Menu.IsDead)
            if (playerMedals == Medals.None)
                LoseGame();
            else
                WinGame();
    }

    void WinGame()
    {
        FinishGame();

        //delay para que se vea el efecto de acercamiento
        Invoke("OpenWin", 1.5f);
    }

    void LoseGame()
    {
        FinishGame();

        //delay para que se vea el efecto de acercamiento
        Invoke("OpenLose", 1.5f);
    }

    void FirstClick()
    {
        StartSpawn();
    }

    // Método que se llama si el jugador gana
    public void OpenWin()
    {
        VictoryScreen.Instance.WinGame();
    }

    // Método que se llama si el jugador muere y no tiene medallas
    public void OpenLose()
    {
        DeathScreen.Instance.LoseGame();
    }

    // Método para chequear las estrellas y la condición de victoria
    public void VictoryCheck(Medals star)
    {
        playerMedals = star;

        if (!Menu.HasWon && !Menu.IsDead)
            if (playerMedals == Medals.ThreeMedal)
                WinGame();
    }

    void SaveGame()
    {
        switch (ScoreManager.Instance.CurrentMedal)
        {
            case Medals.None:
                dataObject.AssignMedals(actualLevel, 0);
                break;
            case Medals.OneMedal:
                dataObject.AssignMedals(actualLevel, 1);
                break;
            case Medals.TwoMedal:
                dataObject.AssignMedals(actualLevel, 2);
                break;
            case Medals.ThreeMedal:
                dataObject.AssignMedals(actualLevel, 3);
                break;
        }

        dataObject.AssignScore(actualLevel, ScoreManager.Instance.TotalScore);

        SaveAndLoad.Save("LevelData", dataObject.Data);
    }

    public void LoadGame()
    {
        SaveData data = (SaveData)SaveAndLoad.Load("LevelData");
        dataObject.Data = data;
    }

    public int ActualLevel { get => actualLevel; set => actualLevel = value; }
    public static GameObject Player => player;
    public static GameManager Instance { get; private set; }
    public static bool HasFinished { get; private set; }
    public DataObject DataObject { get => dataObject; }
}
