using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IPool, IDamagable
{
    [SerializeField] int maxHealthPoints;
    [SerializeField] int scorePoints;

    Vector3 inicialPosition;

    int healthPoints;
    bool isDead;


    public static event EnemyEvent OnDie;
    public delegate void EnemyEvent();

    public int MaxHealthPoints => maxHealthPoints;
    public int HealthPoints => healthPoints;

    public Action OnGettingHurt;

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
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<FieldOfView>().enabled = false;

        transform.position = inicialPosition;
        healthPoints = maxHealthPoints;
    }

    public void TakeDamage()
    {
        if (isDead)
            return;



        healthPoints--;
        isDead = (healthPoints <= 0) ? true : false;

        if (isDead)
        {
            OnDie?.Invoke();

            //aqui van las particulitas, sonidito y eso :v
            if (ScoreManager.Instance)
            {
                OnGettingHurt?.Invoke();

                ScoreManager.Instance.Addscore(scorePoints);
            }

            End();
        }
    }
}
