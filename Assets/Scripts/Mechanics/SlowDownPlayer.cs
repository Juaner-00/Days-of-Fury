using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlayer : MonoBehaviour
{
    PlayerMovementVels movement;

    bool hasCollided = false;

    private void Awake()
    {
        movement = GetComponent<PlayerMovementVels>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasCollided)
            if (other.CompareTag("Remains"))
            {
                movement.IsSlowDown = true;
                hasCollided = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Remains"))
        {
            movement.IsSlowDown = false;
            hasCollided = false;
        }
    }
}