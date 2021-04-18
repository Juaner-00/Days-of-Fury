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

    public static event Action<string, int> Mission = delegate { };
    public static event Action Kill = delegate { };


    Vector3 inicialPosition;

    int healthPoints;
    bool isDead;
    Animator enemyAnimator;
    PoolVfxs particleDamage, particleExplo;

    public static event EnemyEvent OnDie;
    public delegate void EnemyEvent(Vector3 pos);

    public int MaxHealthPoints => maxHealthPoints;
    public int HealthPoints => healthPoints;

    public Action OnGettingHurt;

    EnemyStateMachine stateMachine;
    AIPath aIPath;
    //AIDestinationSetter aIDestinationSetter;
    //EnemyShootController enemyShootController;

    void Awake()
    {
        //enemyShootController = GetComponent<EnemyShootController>();
        //aIDestinationSetter = GetComponent<AIDestinationSetter>();
        stateMachine = GetComponent<EnemyStateMachine>();
        aIPath = GetComponent<AIPath>();
        enemyAnimator = GetComponentInChildren<Animator>();
        particleDamage = GameObject.Find("VFXsChispas(Pool)").GetComponent<PoolVfxs>();
        particleExplo = GameObject.Find("VFXsExplosiones(Pool)").GetComponent<PoolVfxs>();
    }

    // Se llama cuando se instancia el objeto
    public void Instantiate()
    {
        if (aIPath)
        {
            aIPath.usingGravity = false;
            aIPath.canSearch = false;
            aIPath.canMove = false;
            stateMachine.Alive = false;
            //enemyShootController.Dead = true;
        }
        /*if (aIDestinationSetter)
        {
            aIDestinationSetter.target = null;
        }*/
        inicialPosition = transform.position;
    }

    // Se llama cuando el pool devuelve el objeto
    public void Begin(Vector3 position, string tag, Vector3 _)
    {
        if (aIPath)
        {
            aIPath.usingGravity = true;
            aIPath.canSearch = true;
            aIPath.canMove = true;
            stateMachine.Alive = true;
            //enemyShootController.Dead = false;
        }
        /*if (aIDestinationSetter)
        {
            aIDestinationSetter.target = GameManager.Player.transform;
        }*/

        enemyAnimator.SetTrigger("Init");

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
        /*if (aIDestinationSetter)
        {
            aIDestinationSetter.target = null;
        }*/
        transform.position = inicialPosition;
        healthPoints = maxHealthPoints;
    }

    // Método para hacer que el enemigo tome daño
    public void TakeDamage()
    {
        MisionManager mM = MisionManager.Instance; //Piratada. Para tenerlo rapido jiji

        if (isDead)
            return;

        if (damagedSmoke)
        {
            damagedSmoke.Play();
        }

        OnGettingHurt?.Invoke();

        ParticleSystem damage = particleDamage.GetItem(transform.position, tag);
        healthPoints--;
        isDead = (healthPoints <= 0) ? true : false;

        if (isDead)
        {

            if (damagedSmoke)
            {
                damagedSmoke.Stop();
            }

            enemyAnimator.SetTrigger("Dead4");

            if (mM.missions[mM.actualMision].opcion == Missions.Opcion.Enemys) Mission("Enemy", 1); //Sistema de misiones :)

            ParticleSystem Explos = particleExplo.GetItem(transform.position, tag);

            OnDie?.Invoke(transform.position);

            if (ScoreManager.Instance)
            {
                if (mM.missions[mM.actualMision].opcion == Missions.Opcion.Score) Mission("Score", scorePoints); //Sistema de misiones :)
                ScoreManager.Instance.Addscore(scorePoints);
            }
            if (aIPath)
            {
                aIPath.canSearch = false;
                aIPath.canMove = false;
                stateMachine.Alive = false;
                //enemyShootController.Dead = true;
            }
            Invoke("End", timeDead);
            Kill(); //Misiones
        }
    }
}
