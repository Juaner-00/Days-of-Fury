using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject player;

    Vector3 offset;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (player)
            transform.position = player.transform.position + offset;
    }
}
