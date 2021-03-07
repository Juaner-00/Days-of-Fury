using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationTime;
    CamaraShake cShake;


    CharacterController controller;

    float horizontal;
    float vertical;

    public static Action OnMoving;

    private void Awake()
    {
        cShake = GetComponent<CamaraShake>();


        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        OnMoving?.Invoke();

        HandleInputs();
        HandleRotation();

        controller.SimpleMove(transform.forward * speed);
    }

    void HandleInputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void HandleRotation()
    {
        if (vertical > 0.1f)
            transform.DORotate(Vector3.up * 0, rotationTime, RotateMode.Fast);
        else if (vertical < -0.1f)
            transform.DORotate(Vector3.up * 180, rotationTime, RotateMode.Fast);
        else if (horizontal > 0.1f)
            transform.DORotate(Vector3.up * 90, rotationTime, RotateMode.Fast);
        else if (horizontal < -0.1f)
            transform.DORotate(Vector3.up * -90, rotationTime, RotateMode.Fast);
    }




}
