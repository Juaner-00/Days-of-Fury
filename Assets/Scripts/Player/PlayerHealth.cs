using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField] int maxHealthPoints;

    Animator playerAnimator;


    ParticleSystem vfxscontainer;
    VfxsController vfxs;

    PoolVfxs particleDamage, particleExplo;

    public static Action OnDie;
    public static Action <int>OnChangeLife;
    public static Action OnGettingHurt;


    int healthPoints;
    bool isDead;


    private void Awake()
    {
        playerAnimator = GetComponentInChildren<Animator>();
        particleDamage = GameObject.Find("VFXsChispas(Pool)").GetComponent<PoolVfxs>();
        particleExplo = GameObject.Find("VFXsExplosiones(Pool)").GetComponent<PoolVfxs>();
        vfxs = GetComponent<VfxsController>();
        healthPoints = maxHealthPoints;

        isDead = (healthPoints <= 0) ? true : false;
    }

    private void Start()
    {
        OnChangeLife?.Invoke(healthPoints);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
    }

    // Se utiliza para que el jugador tome daño
    public void TakeDamage()
    {
        if (isDead)
            return;

        ParticleSystem damage = particleDamage.GetItem(transform.position, tag);
        OnGettingHurt?.Invoke();
        healthPoints--;
        OnChangeLife?.Invoke(healthPoints);

        foreach (Renderer material in GetComponentsInChildren<Renderer>())
        {
            material.material.SetFloat("damageChanger", (float)healthPoints / maxHealthPoints);
        }

        isDead = (healthPoints <= 0) ? true : false;
        SimpleCameraShakeInCinemachine.Instance.Shake();

        if (isDead)
        {
            if (playerAnimator)
            {
                playerAnimator.SetTrigger("Dead1");
                ParticleSystem Explos = particleExplo.GetItem(transform.position, tag);
            }

            OnDie?.Invoke();
        }
    }

    // Método para que el jugador gane vidas
    public void GainLife()
    {
        healthPoints++;
        OnChangeLife?.Invoke(healthPoints);
    }


    public int MaxHealthPoints => maxHealthPoints;
    public int HealthPoints => healthPoints;
    public bool IsDead => isDead;
}
