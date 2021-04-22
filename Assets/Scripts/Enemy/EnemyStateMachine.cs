using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public enum State
{
    Patrol, Chase, Dead, Heard
}

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] TurretTank turretTank;
    [SerializeField] Transform NoAiming, strafeLeft, strafeRight;
    [SerializeField] float t_minStrafe, t_maxStrafe = 2f, chaseRadius = 38f, ChaseReachedDistance = 20f, fieldOfViewAngle = 20f,
        fovRadius = 55f, memoryStartTime = 5f, noiseTravelDistance = 80f, spinTime = 3f;

    Transform noisePosition;
    Transform[] moveSpots;
    GameObject goMoveSpots;
    int randomSpot, randomStrafeDir;
    float waitTime, startWaitTime, randomStrafeStartTime, waitStrafeTime, distance, inCreasingMemoryTime, isSpiningTime;
    AIDestinationSetter aIDestinationSetter;
    State state;
    GameObject player;
    AIPath aIPath;
    Quaternion target;
    bool playerIsInFov, aiMemorizesPlayer, aiHeardPlayer = false;
    Animator animator;

    [HideInInspector] public bool Alive;
    public Action OnShooting;

    private void Start()
    {
        goMoveSpots = GameObject.Find("PatrolEnemySpots");
        int lenght = goMoveSpots.transform.childCount;
        moveSpots = new Transform[lenght];

        for (int i = 0; i < lenght; i++)
        {
            moveSpots[i] = goMoveSpots.transform.GetChild(i);
        }

        noisePosition = GameObject.Find("NoisePosition").GetComponent<Transform>();
        animator = GetComponentInChildren<Animator>();
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        player = GameManager.Player;
        waitTime = startWaitTime;
        randomSpot = UnityEngine.Random.Range(0, moveSpots.Length);
        aIPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        if (Alive)
        {
            distance = Vector3.Distance(player.transform.position, transform.position);

            if (distance <= fovRadius)
            {
                checkLos();
            }
            if (!playerIsInFov && !aiMemorizesPlayer && !aiHeardPlayer)
            {
                state = State.Patrol;
            }
            else if (aiHeardPlayer && !playerIsInFov && !aiMemorizesPlayer)
            {
                state = State.Heard;
            }
            else if (playerIsInFov)
            {
                aiMemorizesPlayer = true;
                state = State.Chase;
            }
            else if (aiMemorizesPlayer && !playerIsInFov)
            {
                state = State.Chase;
                StartCoroutine(AiMemory());
            }
        }
        else state = State.Dead;
        
        States();
    }

    void States()
    {
        switch (state)
        {
            case State.Patrol:

                TurretPatrol();
                NoiseCheck();
                StopCoroutine(AiMemory());

                aIDestinationSetter.target = moveSpots[randomSpot];

                if (aIPath.reachedDestination)
                {
                    if (waitTime <= 0)
                    {
                        randomSpot = UnityEngine.Random.Range(0, moveSpots.Length);
                        waitTime = startWaitTime;
                    }
                    else waitTime -= Time.deltaTime;
                }
                break;

            case State.Chase:

                TurretAiming();

                if (distance <= chaseRadius && distance > ChaseReachedDistance)
                {
                    aIDestinationSetter.target = player.transform;
                }
                else if (distance <= ChaseReachedDistance)
                {
                    randomStrafeDir = UnityEngine.Random.Range(0, 2);
                    randomStrafeStartTime = UnityEngine.Random.Range(t_minStrafe, t_maxStrafe);

                    //  Audio
                    if(turretTank.Shot()) OnShooting?.Invoke();


                    if (waitStrafeTime <= 0)
                    {
                        if (randomStrafeDir == 0)
                        {
                            aIDestinationSetter.target = strafeLeft;
                        }
                        else if (randomStrafeDir == 1)
                        {
                            aIDestinationSetter.target = strafeRight;
                        }
                    }
                    else waitStrafeTime -= Time.deltaTime;
                }
                break;

            case State.Heard:

                aIDestinationSetter.target = noisePosition;
                
                if (aIPath.reachedDestination)
                {
                    isSpiningTime += Time.deltaTime;

                    if (isSpiningTime >= spinTime)
                    {
                        aiHeardPlayer = false;
                        isSpiningTime = 0f;
                    }
                }
                break;

            case State.Dead:

                aIDestinationSetter.target = null;
                break;
        }
    }

    void TurretAiming()
    {
        Vector3 turretLookDir = player.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0f);
        target = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }

    void TurretPatrol()
    {
        Vector3 turretLookDir = NoAiming.transform.position - turretTank.gameObject.transform.position;
        Vector3 newDir = Vector3.RotateTowards(turretTank.transform.forward, turretLookDir, 1, 0f);
        target = Quaternion.LookRotation(newDir);
        turretTank.transform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
    }

    void NoiseCheck()
    {
        if (distance <= noiseTravelDistance)
        {

            if (Input.GetButton("Fire1"))
            {
                noisePosition.position = player.transform.position;
                aiHeardPlayer = true; 
            }
            else
            {
                aiHeardPlayer = false;
            }
        }
    }

    IEnumerator AiMemory()
    {
        inCreasingMemoryTime = 0;

        while (inCreasingMemoryTime < memoryStartTime)
        {
            inCreasingMemoryTime += Time.deltaTime;
            aiMemorizesPlayer = true;
            yield return null;
        }

        aiHeardPlayer = false;
        aiMemorizesPlayer = false;
    }

    void checkLos()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfViewAngle && angle > -fieldOfViewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, fovRadius))
            {
                if (hit.collider.tag == "Player")
                {
                    playerIsInFov = true;
                    aiMemorizesPlayer = true;
                }
                else playerIsInFov = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fovRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, noiseTravelDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfViewAngle, transform.up) * transform.forward * fovRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfViewAngle, transform.up) * transform.forward * fovRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
}
