using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.AI;

public class FieldOfView : MonoBehaviour
{


    [SerializeField]
    float soundRadius, shotRadius, normalSpeed = 2f, targetSpeed = 3.5f;


    //ramdon AI move
    [SerializeField] int randomXmin, randomXmax, randomZmin, randomZmax;
    [SerializeField]
    Vector3 DestinationPosition;

    //Making a collider in new position
    [SerializeField]
    float nPosColliderDistance;
    public LayerMask nPosColliderMask;





    [SerializeField] TurretTank turretTank;

    Transform targetSound;
    NavMeshAgent agent;
    [SerializeField]
    Transform walkPoint;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    //Look what im looking for
    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    public Action OnMoving;
    public Action OnShooting;

    void Start()
    {

        targetSound = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        NewPosition();

    }



    void Update()
    {
        if (targetSound == null)
        {

            Wander();
            return;

        }


        float distance = Vector3.Distance(targetSound.position, transform.position);

        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);


        //Move to sound radius
        if (distance <= soundRadius)
        {
            GetComponent<NavMeshAgent>().speed = normalSpeed;
            // Move towards the player
            agent.SetDestination(targetSound.position);

        }
        else
        {
            //Move to Wandering Point
            GetComponent<NavMeshAgent>().speed = normalSpeed;
            Wander();

        }
        //Using overlapSphere... no se como usarlo para un solo objetivo
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            //Inside the range 
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                //Visible target
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {

                    //Move to target
                    visibleTargets.Add(target);
                    GetComponent<NavMeshAgent>().speed = targetSpeed;
                    agent.SetDestination(target.position);
                    if (distance <= shotRadius && !Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        // Attack
                        GetComponent<NavMeshAgent>().speed = 0f;
                        turretTank.Shot();
                        OnShooting?.Invoke();
                    }
                }
                else
                {
                    //Move to Wandering Point
                    GetComponent<NavMeshAgent>().speed = normalSpeed;
                    Wander();

                }
            }

        }





    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, soundRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, shotRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(DestinationPosition, nPosColliderDistance);
    }
    void Wander()
    {
        OnMoving?.Invoke();

        if (Vector3.Distance(transform.position, DestinationPosition) <= 0.5f)
        {
            NewPosition();
        }
        else
        {

            agent.SetDestination(DestinationPosition);
        }


        Collider[] wallNoWalking = Physics.OverlapSphere(DestinationPosition, nPosColliderDistance, nPosColliderMask);

        if (wallNoWalking.Length >= 1)
        {
            NewPosition();
        }




    }

    void NewPosition()
    {
        float randomX = UnityEngine.Random.Range(randomXmin, randomXmax);
        float randomZ = UnityEngine.Random.Range(randomZmin, randomZmax);
        DestinationPosition = new Vector3(randomX, transform.position.y, randomZ);


    }
}