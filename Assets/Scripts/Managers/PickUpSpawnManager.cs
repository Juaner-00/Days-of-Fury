using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawnManager : MonoBehaviour
{
    [Header("Spawner Properties")]
    [SerializeField] float spawnTime;
    [SerializeField] float despawnTime;
    [SerializeField] int maxPickUpsAtTime;
    [SerializeField] Transform[] spawnPos;


    [Header("Types of pickUps")]
    [SerializeField] bool health;
    [SerializeField] bool movementSpeed;
    [SerializeField] bool shootSpeed;
    [SerializeField] bool score;


    Pool healthPool;
    Pool movementPool;
    Pool shootPool;
    Pool scorePool;


    List<Pool> pools = new List<Pool>();

    int currentPickUpsInScene;

    float time;
    bool canSpawn;

    // pos, available
    Dictionary<Vector3, bool> spawns = new Dictionary<Vector3, bool>();


    private void OnEnable()
    {
        PickUpBase.OnDespawn += CountPickUps;
        PickUpBase.OnPick += CountPickUps;

    }

    private void OnDisable()
    {
        PickUpBase.OnDespawn -= CountPickUps;
        PickUpBase.OnPick -= CountPickUps;
    }

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        PickUpBase.DespawnTime = despawnTime;

        healthPool = GameObject.Find("Health (Pool)")?.GetComponent<Pool>();
        movementPool = GameObject.Find("Movement (Pool)")?.GetComponent<Pool>();
        shootPool = GameObject.Find("Shoot (Pool)")?.GetComponent<Pool>();
        scorePool = GameObject.Find("Score (Pool)")?.GetComponent<Pool>();

        // Añadir a la lista de pools los que tienen el bool true
        if (health)
            pools.Add(healthPool);
        if (movementSpeed)
            pools.Add(movementPool);
        if (shootSpeed)
            pools.Add(shootPool);
        if (score)
            pools.Add(scorePool);

        // LLenar el diccionario donde están los spawnpoints disponibles
        foreach (Transform trans in spawnPos)
        {
            spawns.Add(trans.position, true);
        }
    }

    private void Update()
    {
        if (time >= spawnTime && currentPickUpsInScene < maxPickUpsAtTime && canSpawn)
        {
            Spawn();
            time = 0;
        }

        time += Time.deltaTime;
    }

    void Spawn()
    {
        // Cantidad de enemigos a spawnear
        int cant = maxPickUpsAtTime - currentPickUpsInScene;

        if (cant > spawnPos.Length)
        {
            cant = spawnPos.Length;
            Debug.LogWarning("No se pueden tener más pickups que puntos de spawn");
        }

        // Spawnear [cant] de enemigos
        for (int i = 0; i < cant; i++)
        {
            if (spawnPos.Length > 0 && pools.Count > 0)
            {
                Vector3 pos = spawnPos[UnityEngine.Random.Range(0, spawnPos.Length)].position;

                if (spawns[pos] == true)
                {
                    // Obtener el enemigo de un pool aleatorio
                    GameObject pick = pools[UnityEngine.Random.Range(0, pools.Count)].GetItem(pos, pos);

                    // Aumetar el contador de enemigos vivos
                    currentPickUpsInScene++;
                    // print("Spawn");

                    spawns[pos] = false;
                }
                else
                    i--;
            }
        }

    }

    // Parar de spawnear
    public void StopSpawn()
    {
        canSpawn = false;
    }

    // Resumir el spawn de pickups
    public void ResumeSpawn()
    {
        canSpawn = true;
    }

    // Empieza a spawnear los pickups
    public void StartSpawning()
    {
        canSpawn = true;
        time = spawnTime;
    }

    void CountPickUps(Vector3 pos, PickUpType pType)
    {
        spawns[pos] = true;
        currentPickUpsInScene--;
    }

    void CountPickUps(Vector3 pos)
    {
        spawns[pos] = true;
        currentPickUpsInScene--;
    }

    public static PickUpSpawnManager Instance { get; private set; }
    public int PickUpsInScene => currentPickUpsInScene;
    public bool CanSpawn => canSpawn;
}
