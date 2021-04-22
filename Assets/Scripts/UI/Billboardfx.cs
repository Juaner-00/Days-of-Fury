
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboardfx : MonoBehaviour
{
    
     Transform camTransform;
     Transform player;
    Quaternion originalRotation;

    void Start()
    {
        player = GetComponentInParent<PlayerHealth>().gameObject.transform;
        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        
        transform.rotation = camTransform.rotation * originalRotation;

        //transform.LookAt(camTransform);

        transform.position = player.position;
    }
}