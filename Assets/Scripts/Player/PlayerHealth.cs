using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] int maxHealthPoints;
    [SerializeField] SimpleCameraShakeInCinemachine sCamara;
 
    Animator playerAnimator;


    ParticleSystem vfxscontainer;
    CinemachineImpulseSource impulseS;
    VfxsController vfxs;

    PoolVfxs particleDamage, particleExplo;
    public static Action OnDie;
    public static Action OnChangeLife;
    public static Action OnGettingHurt;

    int healthPoints;
    bool isDead;


    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        particleDamage = GameObject.Find("VFXsChispas(Pool)").GetComponent<PoolVfxs>();
        particleExplo = GameObject.Find("VFXsExplosiones(Pool)").GetComponent<PoolVfxs>();
        vfxs = GetComponent<VfxsController>();
        impulseS = GetComponent<CinemachineImpulseSource>();
        healthPoints = maxHealthPoints;
        isDead = (healthPoints <= 0) ? true : false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();

        }
    }

    public void TakeDamage()
    {

        if (isDead)
            return;


        ParticleSystem damage = particleDamage.GetItem(transform.position, tag);
        OnGettingHurt?.Invoke();
        healthPoints--;
        OnChangeLife?.Invoke();


        isDead = (healthPoints <= 0) ? true : false;
        //impulseS.GenerateImpulse();
        sCamara.Shake();

        if (isDead)
        {
            if (playerAnimator)
            {
                playerAnimator.SetTrigger("Dead1");
                ParticleSystem Explos = particleExplo.GetItem(transform.position, tag);
            }
               
            OnDie?.Invoke();
            // Cuando se tenga la animación y las partículas de muerte se cambia el Destroy
            //Destroy(gameObject);
        }
    }

    public void GainLife()
    {
        healthPoints++;
        OnChangeLife?.Invoke();
    }


    public int MaxHealthPoints => maxHealthPoints;
    public int HealthPoints => healthPoints;
    public bool IsDead => isDead;
}
