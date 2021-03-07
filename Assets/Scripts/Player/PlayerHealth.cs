using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] int maxHealthPoints;
    [SerializeField] SimpleCameraShakeInCinemachine sCamara;
    [SerializeField] ParticleSystem chispa, explosion;
    [SerializeField] Animator player;


    ParticleSystem vfxscontainer;
    CinemachineImpulseSource impulseS;
    VfxsController vfxs;

    public static Action OnDie;
    public static Action OnChangeLife;
    public static Action OnGettingHurt;

    int healthPoints;
    bool isDead;


    private void Awake()
    {
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
            if (chispa)
            {
                ParticleSystem daño = Instantiate(chispa, transform.position, transform.rotation);
                daño.Play();
            }
        }
    }

    public void TakeDamage()
    {
        OnGettingHurt?.Invoke();

        if (isDead)
            return;

        healthPoints--;
        OnChangeLife?.Invoke();


        isDead = (healthPoints <= 0) ? true : false;
        //impulseS.GenerateImpulse();
        sCamara.Shake();

        if (isDead)
        {
            if (player)
                player.SetTrigger("Dead1");
            OnDie?.Invoke();
            // Cuando se tenga la animación y las partículas de muerte se cambia el Destroy
            Destroy(gameObject);
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
