using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementVels : MonoBehaviour
{
    [Header("Script Properties")]
    [SerializeField] float speedBase;
    [SerializeField] float rotationTime;

    CharacterController controller;

    float horizontal;
    float vertical;

    [SerializeField, Header("Debug")]
    float movementSpeed;

    public static Action OnMoving;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        movementSpeed = speedBase;
    }

    private void Update()
    {
        OnMoving?.Invoke();

        HandleInputs();
        HandleRotation();

        controller.SimpleMove(transform.forward * movementSpeed);
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

    // Método para aumentar la velocidad de movimiento
    public void GainSpeed(float porcent)
    {
        movementSpeed += movementSpeed * porcent / 100;
    }
}


public enum PlayerStates
{
    Stoped,
    SlowAcceleration,
    FastAcceleration
}