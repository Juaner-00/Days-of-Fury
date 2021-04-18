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
    [SerializeField] Transform[] moveSpots;
    [SerializeField] TurretTank turretTank;
    [SerializeField] Transform NoAiming, strafeLeft, strafeRight;
    [SerializeField] float t_minStrafe, t_maxStrafe, chaseRadius = 20f, facePlayerFactor = 20f,
        moveSpotReachedDistance = 5f, ChaseReachedDistance = 20f, fieldOfViewAngle = 160f, losRadius = 45f,
        memoryStartTime = 10f, noiseTravelDistance, spinSpeed = 3f, spinTime = 3f;

    int randomSpot, randomStrafeDir;
    float waitTime, startWaitTime, randomStrafeStartTime, waitStrafeTime, distance, inCreasingMemoryTime, isSpiningTime;
    AIDestinationSetter aIDestinationSetter;
    State state;
    GameObject player;
    AIPath aIPath;
    Quaternion target;
    Transform noisePosition;
    bool playerIsInLos, aiMemorizesPlayer, aiHeardPlayer = false, canSpin = false;

    public bool Alive;
    public Action OnShooting;

    private void Start()
    {
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

            /*if (distance > chaseRadius)
            {
                state = State.Patrol;
            }
            else if (distance <= chaseRadius)
            {
                state = State.Chase;
            }*/
            if (distance <= losRadius)
            {
                checkLos();
            }
            if (!playerIsInLos && !aiMemorizesPlayer && !aiHeardPlayer)
            {
                state = State.Patrol;
            }
            else if (aiHeardPlayer && !playerIsInLos && !aiMemorizesPlayer)
            {
                canSpin = true;
                state = State.Heard;
            }
            else if (playerIsInLos)
            {
                aiMemorizesPlayer = true;
                state = State.Chase;
            }
            else if (aiMemorizesPlayer && !playerIsInLos)
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
                //aIPath.endReachedDistance = moveSpotReachedDistance;

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
                //aIPath.endReachedDistance = ChaseReachedDistance;

                if (distance <= chaseRadius && distance > ChaseReachedDistance)
                {
                    aIDestinationSetter.target = player.transform;
                }
                else if (distance <= ChaseReachedDistance)
                {
                    randomStrafeDir = UnityEngine.Random.Range(0, 2);
                    randomStrafeStartTime = UnityEngine.Random.Range(t_minStrafe, t_maxStrafe);

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

                //aIDestinationSetter = POS SONIDO
                
                if (Vector3.Distance(transform.position, noisePosition.position) <= 5f && canSpin)
                {
                    isSpiningTime += Time.deltaTime;
                    transform.Rotate(Vector3.up * spinSpeed, Space.World);

                    if (isSpiningTime >= spinTime)
                    {
                        canSpin = false;
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
            //posicion de sonido
        }
        else
        {
            aiHeardPlayer = false;
            canSpin = false;
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

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, losRadius))
            {
                if (hit.collider.tag == "Player")
                {
                    playerIsInLos = true;
                    aiMemorizesPlayer = true;
                }
                else playerIsInLos = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        
    }
}
