using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class NewEnemyShoot : MonoBehaviour
{
    [SerializeField] TurretTank turretTank;
    AIPath aIPath;
    public LayerMask obstacleMask;
    public Action OnShooting;
    public bool Dead;
    Quaternion target;

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
        if (!Dead)
        {
            Vector3 turretLookDir = GameManager.Player.transform.position - turretTank.gameObject.transform.position;
            Vector3 newDir = Vector3.RotateTowards(turretTank.gameObject.transform.forward, turretLookDir, 1, 0.0F);
            target = Quaternion.LookRotation(newDir);
            turretTank.gameObject.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
        }
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
