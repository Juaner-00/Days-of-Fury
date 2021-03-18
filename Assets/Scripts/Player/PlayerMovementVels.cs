using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementVels : MonoBehaviour
{
    [Header("Script Properties")]
    [SerializeField] float maxSpeedBase;
    [SerializeField] float acceleration;
    [SerializeField] float rotationTime;


    [Header("Debug")]
    [SerializeField] float movementSpeed;
    [SerializeField] float maxSpeed;


    float horizontal;
    float vertical;

    CharacterController controller;

    PlayerStates state;

    public static Action OnMoving;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        state = PlayerStates.Stoped;
        movementSpeed = 0;
        maxSpeed = maxSpeedBase;
    }

    private void Update()
    {
        HandleInputs();
        HandleRotation();

        HandleRayCast();
        HandleSpeed();
    }

    void HandleRayCast()
    {

    }

    void HandleSpeed()
    {
        if (movementSpeed > maxSpeed)
        {
            movementSpeed = maxSpeed;
            state = PlayerStates.MaxSpeed;
        }

        switch (state)
        {
            case PlayerStates.Stoped:
                if (Input.anyKey)
                    state = PlayerStates.Accelerating;
                break;
            case PlayerStates.Accelerating:
                OnMoving?.Invoke();
                movementSpeed += acceleration * Time.deltaTime;
                controller.SimpleMove(transform.forward * movementSpeed);
                break;
            case PlayerStates.MaxSpeed:
                OnMoving?.Invoke();
                controller.SimpleMove(transform.forward * movementSpeed);
                break;
        }
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
        maxSpeed += maxSpeed * porcent / 100;
    }

}


public enum PlayerStates
{
    Stoped,
    Accelerating,
    MaxSpeed
}