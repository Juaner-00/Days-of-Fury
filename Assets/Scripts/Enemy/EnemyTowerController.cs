using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyTowerController : MonoBehaviour, IDamagable
{
    [SerializeField] int maxHealthPoints, scorePoints;
    [SerializeField] ParticleSystem damagedSmoke;
    [SerializeField] float radius, timeDead;
    [SerializeField] TurretTank turretTank;

    GameObject player;
    Quaternion target;
    int healthPoints;
    PoolVfxs particleDamage, particleExplo;
    bool isDead;
    Animator enemyAnimator;

    public int MaxHealthPoints => maxHealthPoints;
    public int HealthPoints => healthPoints;
    public static event Action<int> Mission = delegate { };
    public static event Action Kill = delegate { };
    public static event EnemyEvent OnDie;
    public delegate void EnemyEvent(Vector3 pos);

    #region Sound

    public Action OnShooting;
    public Action OnGettingHurt;

    #endregion

    void Awake()
    {
        particleDamage = GameObject.Find("VFXsChispas(Pool)").GetComponent<PoolVfxs>();
        particleExplo = GameObject.Find("VFXsExplosiones(Pool)").GetComponent<PoolVfxs>();
    }

    void Start()
    {
        isDead = false;
        enemyAnimator = GetComponentInChildren<Animator>();
        player = GameManager.Player;
        healthPoints = maxHealthPoints;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (!isDead)
        {
            TurretAiming();
            if (distance <= radius)
            {
                enemyAnimator.SetTrigger("Shooting");
                if(turretTank.Shot()) OnShooting?.Invoke();
            } 
        }
    }

    void TurretAiming()
    {
        Vector3 turretLookDir = player.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0f);
        target = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }

    public void TakeDamage()
    {
        MisionManager mM = MisionManager.Instance;
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

            enemyAnimator.SetTrigger("Dead");

            ParticleSystem Explos = particleExplo.GetItem(transform.position, tag);

            OnDie?.Invoke(transform.position);

            if (ScoreManager.Instance)
            {
                ScoreManager.Instance.Addscore(scorePoints);
            }
            Kill(); //Misiones
            Destroy(gameObject, timeDead);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
