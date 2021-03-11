using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;
    [SerializeField] int scoreToWin;

    [SerializeField] bool spawnEnemies;
    [SerializeField] bool spawnPickUps;

    bool hasFinished;


    static GameObject player;

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;

        hasFinished = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2, cursorImage.height / 2), CursorMode.Auto);

        if (spawnEnemies)
            EnemySpawnManager.Instance.StartSpawning();

        if (spawnPickUps)
            PickUpSpawnManager.Instance.StartSpawning();
    }

    private void OnEnable()
    {
        PlayerHealth.OnDie += LoseGame;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDie -= LoseGame;
    }

    private void Update()
    {
        // Si ya se terminó el juego no haga nada
        if (!hasFinished)
        {
            // Ganar por la cantidad de score
            if (ScoreManager.Instance)
                if (ScoreManager.Instance.TotalScore >= scoreToWin)
                    WinGame(player.transform.position);

            if (spawnEnemies)
                // Parar de spawnear si ya alcanzó la cantidad de enemigos matado
                if (EnemySpawnManager.Instance.EnemiesKilled >= EnemySpawnManager.Instance.EnemiesToStopSpawn)
                    EnemySpawnManager.Instance.StopSpawn();

            // Ganar el juego si ya no hay enemigos vivos 
            if (spawnEnemies && !EnemySpawnManager.Instance.CanSpawn && EnemySpawnManager.Instance.EnemiesAlived <= 0)
                WinGame(EnemySpawnManager.LastPos);
        }
    }

    public void FinishGame()
    {
        if (player)
        {
            player.GetComponent<SCT_TankMovement>().enabled = false;
            player.GetComponent<ReticleController>().enabled = false;
        }

        if (EnemySpawnManager.Instance)
            EnemySpawnManager.Instance.StopSpawn();

        if (PickUpSpawnManager.Instance)
            PickUpSpawnManager.Instance.StopSpawn();

        hasFinished = true;
    }

    public void LoseGame()
    {
        FinishGame();
        //delay para que se vea la muerte del player
        Invoke("deadTank", 2f);

    }

    public void WinGame(Vector3 pos)
    {
        CamaraManager.Instance.ChangeCam(pos);
        FinishGame();
        //delay para que se vea el efecto de acercamiento
        Invoke("WiningTank", 2f);
    }




    public void WiningTank()
    {
        VictoryScreen.Instance.WinGame();
    }
    public void deadTank()
    {
        DeathScreen.Instance.LoseGame();
    }

    public static GameObject Player => player;
    public static GameManager Instance { get; private set; }
}
