using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public enum States
{
    Dead,
    Patrol,
    Listen,
    Chasing,
    Attack,
}

public class EnemyStateMachine : MonoBehaviour
{
    public States state;

    Vector3 dirTarget;
    Quaternion targetAim;
    Transform target;
    GameObject player;
    AIDestinationSetter aIDestinationSetter;

    [SerializeField] TurretTank turretTank;
    [SerializeField] Transform NoAiming;
    [SerializeField] float soundRadius, shotRadius, normalSpeed = 5f, targetSpeed = 8f;

    Transform[] wayPoints;

    public Action OnShooting;

    private void Start()
    {
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        player = GameManager.Player;
    }

    private void Update()
    {
        SwitchStates();
    }

    void SwitchStates()
    {
        switch (state)
        {
            case States.Patrol:
                //target = ;
                ChangeTarget(target);
                TurretPatrol();
                break;
            case States.Listen:
                //target = ;
                ChangeTarget(target);
                break;
            case States.Chasing:
                ChangeTarget(player.transform);
                TurretAiming();
                break;
            case States.Attack:
                turretTank.Shot();
                OnShooting?.Invoke();
                break;
            case States.Dead:
                break;
        }
    }

    void ChangeTarget(Transform newTarget)
    {
        aIDestinationSetter.target = newTarget;
    }

    void TurretAiming()
    {
        Vector3 turretLookDir = player.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0.0F);
        targetAim = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }

    void TurretPatrol()
    {
        Vector3 turretLookDir = NoAiming.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0.0F);
        targetAim = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }
}
