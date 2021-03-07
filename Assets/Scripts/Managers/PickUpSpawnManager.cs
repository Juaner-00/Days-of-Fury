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


    Pool healthPool;
    Pool movementPool;
    Pool shootPool;


    List<Pool> pools = new List<Pool>();

    int currentPickUpsInScene;

    float time;
    bool canSpawn;


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
        shootPool = GameObject.Find("shoot (Pool)")?.GetComponent<Pool>();

        // Añadir a la lista de pools los que tienen el bool true
        if (health)
            pools.Add(healthPool);
        if (movementSpeed)
            pools.Add(movementPool);
        if (shootSpeed)
            pools.Add(shootPool);
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

        // Spawnear [cant] de enemigos
        for (int i = 0; i < cant; i++)
        {
            if (spawnPos.Length > 0 && pools.Count > 0)
            {
                Vector3 pos = spawnPos[UnityEngine.Random.Range(0, spawnPos.Length)].position;

                // Obtener el enemigo de un pool aleatorio
                GameObject enemy = pools[UnityEngine.Random.Range(0, pools.Count)].GetItem(pos);

                // Aumetar el contador de enemigos vivos
                currentPickUpsInScene++;
                // print("Spawn");
            }
        }

    }

    public void StopSpawn()
    {
        canSpawn = false;
    }

    public void ResumeSpawn()
    {
        canSpawn = true;
    }

    public void StartSpawning()
    {
        canSpawn = true;
        time = 0;
    }

    void CountPickUps()
    {
        currentPickUpsInScene--;
        time = 0;
    }


    public static PickUpSpawnManager Instance { get; private set; }
    public int PickUpsInScene => currentPickUpsInScene;
    public bool CanSpawn => canSpawn;
}
