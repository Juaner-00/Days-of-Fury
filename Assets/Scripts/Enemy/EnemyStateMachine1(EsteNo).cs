using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public enum States1
{
    Dead,
    Patrol,
    Listen,
    Chasing,
    Attack,
}

public class EnemyStateMachine1 : MonoBehaviour
{
    public States1 state;

    Vector3 dirTarget;
    Quaternion targetAim;
    Transform target;
    GameObject player;
    AIDestinationSetter aIDestinationSetter;
    
    [SerializeField] LayerMask targetMask, obstacleMask;
    [SerializeField] TurretTank turretTank;
    [SerializeField] Transform NoAiming;
    [SerializeField] float soundRadius, viewRadius, shotRadius, normalSpeed = 5f, targetSpeed = 8f;

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
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, soundRadius, targetMask);

        switch (state)
        {
            case States1.Patrol:

                TurretPatrol();

                if (Vector3.Distance(transform.position, target.position) <= 0.5f)
                {
                    NewPosition();
                }



                break;
            case States1.Listen:
                //target = ;
                ChangeTarget(target);
                break;
            case States1.Chasing:
                ChangeTarget(player.transform);
                TurretAiming();
                break;
            case States1.Attack:
                turretTank.Shot();
                OnShooting?.Invoke();
                break;
            case States1.Dead:
                break;
        }
    }

    public void ChangeStatePatrol()
    {
        state = States1.Patrol;
        NewPosition();
        ChangeTarget(target);
    } 

    void ChangeTarget(Transform newTarget)
    {
        aIDestinationSetter.target = newTarget;
    }

    void TurretAiming()
    {
        Vector3 turretLookDir = player.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0f);
        targetAim = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }

    void TurretPatrol()
    {
        Vector3 turretLookDir = NoAiming.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0f);
        targetAim = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }

    void NewPosition()
    {
        int i = UnityEngine.Random.Range(0, wayPoints.Length);
        target = wayPoints[i];
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soundRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shotRadius);
    }
}
