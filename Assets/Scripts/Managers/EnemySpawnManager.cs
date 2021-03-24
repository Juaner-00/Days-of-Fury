using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Spawner Properties")]
    [Tooltip("Tiempo que pasa para spawnear después de matar")]
    [SerializeField] float spawnTime;
    [SerializeField] int totalEnemiesToKill;
    [SerializeField] int maxEnemiesAtTime;
    [SerializeField] Transform[] spawnPos;


    [Header("Types of enemies")]
    [SerializeField] bool normalEnemy;
    [SerializeField] bool robustTank;
    [SerializeField] bool turret;


    Pool normalEnemyPool;
    Pool robustTankPool;
    Pool turretPool;

    static Vector3 lastPos;

    List<Pool> pools = new List<Pool>();

    int enemiesKilled;
    int currentEnemiesAlive;

    float time;
    bool canSpawn;


    private void OnEnable()
    {
        EnemyController.OnDie += CountKilled;
    }

    private void OnDisable()
    {
        EnemyController.OnDie -= CountKilled;
    }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {

        normalEnemyPool = GameObject.Find("Normal Enemy (Pool)")?.GetComponent<Pool>();
        robustTankPool = GameObject.Find("Robust Tank (Pool)")?.GetComponent<Pool>();
        turretPool = GameObject.Find("Turret (Pool)")?.GetComponent<Pool>();

        time = spawnTime;

        // Añadir a la lista de pools los que tienen el bool true
        if (normalEnemy)
            pools.Add(normalEnemyPool);
        if (robustTank)
            pools.Add(robustTankPool);
        if (turret)
            pools.Add(turretPool);
    }

    private void Update()
    {
        if (enemiesKilled + currentEnemiesAlive < totalEnemiesToKill)
        {
            if (time >= spawnTime && currentEnemiesAlive < maxEnemiesAtTime && canSpawn)
            {
                Spawn();
                time = 0;
            }
        }
        else
            StopSpawn();

        time += Time.deltaTime;
    }

    void Spawn()
    {
        int cant = 0;

        // Cantidad de enemigos a spawnear
        if (enemiesKilled + maxEnemiesAtTime <= totalEnemiesToKill)
        {
            cant = maxEnemiesAtTime - currentEnemiesAlive;
        }
        else
        {
            cant = totalEnemiesToKill - enemiesKilled;
        }

        // Spawnear [cant] de enemigos
        for (int i = 0; i < cant; i++)
        {
            if (spawnPos.Length > 0 && pools.Count > 0)
            {
                Vector3 pos = spawnPos[Random.Range(0, spawnPos.Length)].position;

                // Obtener el enemigo de un pool aleatorio
                GameObject enemy = pools[Random.Range(0, pools.Count)].GetItem(pos, Vector3.zero);

                // Aumetar el contador de enemigos vivos
                currentEnemiesAlive++;
            }
        }
    }

    // Parar que se puedan spawnear enemigos
    public void StopSpawn()
    {
        canSpawn = false;
    }

    // Resumir que se puedan spawnear enemigos
    public void ResumeSpawn()
    {
        canSpawn = true;
    }

    // Empieza a spawnear enemigos
    public void StartSpawning()
    {
        canSpawn = true;
        // time = 0;
    }

    void CountKilled(Vector3 pos)
    {
        lastPos = pos;
        currentEnemiesAlive--;
        enemiesKilled++;

        time = 0;
    }


    public static Vector3 LastPos => lastPos;
    public static EnemySpawnManager Instance { get; private set; }
    public int EnemiesKilled => enemiesKilled;
    public int EnemiesAlived => currentEnemiesAlive;
    public int TotalEnemiesToKill => totalEnemiesToKill;
    public bool CanSpawn => canSpawn;
}
