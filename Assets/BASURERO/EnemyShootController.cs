using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyShootController : MonoBehaviour
{
    [SerializeField] TurretTank turretTank;
    //[SerializeField] Transform NoAiming;
    
    AIPath aIPath;
    public Action OnShooting;
    Quaternion target;

    [HideInInspector] public bool Dead;
    [HideInInspector] public bool Aiming;

    private void Start()
    {
        aIPath = GetComponent<AIPath>();
        Dead = false;
    }

    private void Update()
    {
        HandleTurret();
        if (!Dead)
        {
            if (aIPath.reachedDestination)
            {
                turretTank.Shot();
                OnShooting?.Invoke();
            }
        }
    }

    void HandleTurret()
    {
        if (!Dead /*&& Aiming*/)
        {
            Vector3 turretLookDir = GameManager.Player.transform.position - turretTank.gameObject.transform.position;
            Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0.0F);
            target = Quaternion.LookRotation(newDir);
            turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
        }/*
        else if (!Dead)
        {
            Vector3 turretLookDir = NoAiming.transform.position - turretTank.gameObject.transform.position;
            Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0.0F);
            target = Quaternion.LookRotation(newDir);
            turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
        }
        */
    }

    void changeBool()
    {
        Dead = true;
    }

    private void OnEnable()
    {
        PlayerHealth.OnDie += changeBool;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDie -= changeBool;
    }
}
