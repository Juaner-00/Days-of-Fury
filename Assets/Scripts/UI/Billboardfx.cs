
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboardfx : MonoBehaviour
{
    public Transform camTransform;
    public Transform player;
    Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;

    }

    void Update()
    {
        transform.rotation = camTransform.rotation * originalRotation;
        transform.position = player.position;
    }
}