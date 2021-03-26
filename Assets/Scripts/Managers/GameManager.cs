using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorImage;

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

        playerMedals = Medals.None;
    }

    private void OnEnable()
    {
        PlayerHealth.OnDie += PlayerDie;
        ScoreManager.OnStarObtained += VictoryCheck;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDie -= PlayerDie;
        ScoreManager.OnStarObtained -= VictoryCheck;
    }

    // Método para finalizar el juego
    public void FinishGame()
    {
        if (player)
        {
            if (player.name == "Player")
                player.GetComponentInParent<PlayerMovement>().enabled = false;
            else if (player.name == "Player 2")
                player.GetComponentInParent<PlayerMovementVels>().enabled = false;
            else
                player.GetComponentInParent<SCT_TankMovement>().enabled = false;
            player.GetComponentInParent<ReticleController>().enabled = false;

            if (CamaraManager.Instance)
                CamaraManager.Instance.ChangeCam(player.transform.position);
        }

        if (EnemySpawnManager.Instance)
            EnemySpawnManager.Instance.StopSpawn();

        if (PickUpSpawnManager.Instance)
            PickUpSpawnManager.Instance.StopSpawn();

        hasFinished = true;
    }

    // Método que se cuando el jugador muere
    void PlayerDie()
    {
        FinishGame();

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

        if (playerMedals == Medals.ThreeMedal)
            WinGame();
    }


    public static GameObject Player => player;
    public static GameManager Instance { get; private set; }
}
