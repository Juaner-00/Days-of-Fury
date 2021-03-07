using UnityEngine;
using UnityEngine.AI;
using System.Collections;

/* Makes enemies follow and attack the player */
public class EnemyM : MonoBehaviour
{
    [SerializeField]
    float lookRadius, soundRadius;

    Transform target;
    NavMeshAgent agent;
    [SerializeField]
    Transform walkPoint;


    private void OnEnable()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            GetComponent<NavMeshAgent>().speed = 3.5f;
            // Move towards the player
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                // Attack

            }
        }
        else if (distance <= soundRadius)
        {
            GetComponent<NavMeshAgent>().speed = 2f;
            // Move towards the sound
            agent.SetDestination(target.position);
        }
        else
        {
            GetComponent<NavMeshAgent>().speed = 2f;
            agent.SetDestination(walkPoint.position);

        }
    }

    // Point towards the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}