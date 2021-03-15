using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Hace que el enemigo se mueva y ataque al jugador */
public class EnemyM : MonoBehaviour
{
    [SerializeField]
    float lookRadius, soundRadius;

    [SerializeField]

    Transform walkPoint;
    Transform target;
    NavMeshAgent agent;


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.speed = 3.5f;
            // Moverse hacia el jugador
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                // Atacar

            }
        }
        else if (distance <= soundRadius)
        {
            agent.speed = 2f;
            // Se mueve hacia el sonido
            agent.SetDestination(target.position);
        }
        else
        {
            agent.speed = 2f;
            agent.SetDestination(walkPoint.position);
        }
    }

    // Apunta al jugador
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}
