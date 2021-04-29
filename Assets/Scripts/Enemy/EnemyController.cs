using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class EnemyController : MonoBehaviour, IPool, IDamagable
{
    [SerializeField] int maxHealthPoints;
    [SerializeField] int scorePoints;
    [SerializeField] float timeDead;
    [SerializeField] ParticleSystem damagedSmoke;
    [SerializeField, Range(0, 1)] float stayOnLevelProbability;

    Renderer[] materialRobust;

    public static event Action<int> Mission = delegate { };
    public static event Action Kill = delegate { };

    #region Sound

    public Action OnGettingHurt;

    #endregion

    Vector3 inicialPosition;

    int healthPoints;
    bool isDead;
    Animator enemyAnimator;
    PoolVfxs particleDamage, particleExplo;

    public static event EnemyEvent OnDie;
    public delegate void EnemyEvent(Vector3 pos);

    public int MaxHealthPoints => maxHealthPoints;
    public int HealthPoints => healthPoints;


    AIPath aIPath;
    EnemyStateMachine stateMachine;

    void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        aIPath = GetComponent<AIPath>();
        enemyAnimator = GetComponentInChildren<Animator>();
        particleDamage = GameObject.Find("VFXsChispas(Pool)").GetComponent<PoolVfxs>();
        particleExplo = GameObject.Find("VFXsExplosiones(Pool)").GetComponent<PoolVfxs>();
        materialRobust = GetComponentsInChildren<Renderer>();
    }

    // Se llama cuando se instancia el objeto
    public void Instantiate()
    {
        if (aIPath)
        {
            aIPath.usingGravity = false;
            aIPath.canSearch = false;
            aIPath.canMove = false;
        }

        stateMachine.Alive = false;
        inicialPosition = transform.position;

        float prob = UnityEngine.Random.Range(0f, 1f);
        StayOnScene = prob <= stayOnLevelProbability;
    }

    // Se llama cuando el pool devuelve el objeto
    public void Begin(Vector3 position, string tag, Vector3 _)
    {
        if (aIPath)
        {
            aIPath.usingGravity = true;
            aIPath.canSearch = true;
            aIPath.canMove = true;
        }

        enemyAnimator.SetTrigger("Init");

        stateMachine.Alive = true;
        healthPoints = maxHealthPoints;
        isDead = false;
        transform.position = position;
    }

    // Se llama cuando el objeto se devuelve al pool
    public void End()
    {
        if (aIPath)
        {
            aIPath.usingGravity = false;
        }

        if (!StayOnScene)
        {
            transform.position = inicialPosition;
            healthPoints = maxHealthPoints;
            enemyAnimator.SetTrigger("Init");
        }
    }

    // Método para hacer que el enemigo tome daño
    public void TakeDamage()
    {

        if (isDead)
            return;

        if (damagedSmoke)
        {
            damagedSmoke.Play();
        }

        OnGettingHurt?.Invoke();

        ParticleSystem damage = particleDamage.GetItem(transform.position, tag);
        healthPoints--;
        foreach (Renderer mr in materialRobust)
        {
            mr.material.SetFloat("damageRobust", (float)healthPoints / maxHealthPoints);
        }

        isDead = (healthPoints <= 0) ? true : false;

        if (isDead)
        {
            stateMachine.Alive = false;
            
            if (damagedSmoke)
            {
                damagedSmoke.Stop();
            }

            if (StayOnScene)
                enemyAnimator.SetTrigger($"Dead{UnityEngine.Random.Range(3, 5)}");
            else
                enemyAnimator.SetTrigger($"Dead{UnityEngine.Random.Range(1, 5)}");


            ParticleSystem Explos = particleExplo.GetItem(transform.position, tag);

            OnDie?.Invoke(transform.position);

            if (ScoreManager.Instance)
            {
                ScoreManager.Instance.Addscore(scorePoints);
            }
            if (aIPath)
            {
                aIPath.canSearch = false;
                aIPath.canMove = false;
            }
            Invoke("End", timeDead);
            Kill(); //Misiones
        }
    }

    public bool StayOnScene { get; set; }
}
