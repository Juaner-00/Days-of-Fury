using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IPool, IDamagable
{
    [SerializeField] int maxHealthPoints;
    [SerializeField] int scorePoints;
    [SerializeField] float timeDead;
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

    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        particleDamage = GameObject.Find("VFXsChispas(Pool)").GetComponent<PoolVfxs>();
        particleExplo = GameObject.Find("VFXsExplosiones(Pool)").GetComponent<PoolVfxs>();
    }
    public void Instantiate()
    {
        GetComponent<FieldOfView>().enabled = false;
        inicialPosition = transform.position;
    }

    public void Begin(Vector3 position, string tag)
    {
        healthPoints = maxHealthPoints;
        isDead = false;
        transform.position = position;

        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<FieldOfView>().enabled = true;
    }

    public void End()
    {


        transform.position = inicialPosition;
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage()
    {
        if (isDead)
            return;


        ParticleSystem damage = particleDamage.GetItem(transform.position, tag);
        healthPoints--;
        isDead = (healthPoints <= 0) ? true : false;

        if (isDead)
        {
            enemyAnimator.SetTrigger("Dead4");
            ParticleSystem Explos = particleExplo.GetItem(transform.position, tag);

            OnDie?.Invoke(transform.position);
            

            //aqui van las particulitas, sonidito y eso :v
            if (ScoreManager.Instance)
            {
                OnGettingHurt?.Invoke();

                ScoreManager.Instance.Addscore(scorePoints);
            }
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<FieldOfView>().enabled = false;
            Invoke("End", timeDead);
        }
    }
}
