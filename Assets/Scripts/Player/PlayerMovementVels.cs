using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementVels : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float maxSpeedBase;
    [SerializeField] float acceleration;
    [SerializeField] float crashCoolDown;
    [SerializeField] float rotationTime;

    [Header("Ray Properties")]
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] float frontLenght;
    [SerializeField] float leftLenght;
    [SerializeField] float rightLenght;


    [Header("Debug")]
    [SerializeField] float movementSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float crashCoolDownTimer;


    float horizontal;
    float vertical;

    CharacterController controller;

    PlayerStates state;

    Ray frontRay;
    Ray leftRay;
    Ray rightRay;


    public static Action OnMoving;
    public static Action OnStoped;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        state = PlayerStates.Stoped;
        movementSpeed = 0;
        maxSpeed = maxSpeedBase;

        crashCoolDownTimer = crashCoolDown;

        frontRay = new Ray(transform.position, transform.forward * frontLenght);
        leftRay = new Ray(transform.position, transform.right * -1 * leftLenght);
        rightRay = new Ray(transform.position, transform.right * rightLenght);
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
        // Si no tiene el coolDown
        if (crashCoolDownTimer > crashCoolDown)
        {
            // Choca con el rayo del frente o el de la derecha o izquierda
            if (Physics.Raycast(frontRay, frontLenght, obstacleMask) ||
                Physics.Raycast(leftRay, leftLenght, obstacleMask) ||
                Physics.Raycast(rightRay, rightLenght, obstacleMask))
            {
                switch (state)
                {
                    // Si está acelerando se para
                    case PlayerStates.Accelerating:
                        state = PlayerStates.Stoped;
                        movementSpeed = 0;
                        OnStoped?.Invoke();
                        break;
                    // Si tiene la máxima velocidad se le reduce y se pone en el estado acelerando
                    case PlayerStates.MaxSpeed:
                        state = PlayerStates.Accelerating;
                        movementSpeed *= 0.3f;
                        break;
                }

                crashCoolDownTimer = 0;
            }
        }

        crashCoolDownTimer += Time.deltaTime;
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
            // Si está parado y presiona cualquier tecla se pone en acelerando
            case PlayerStates.Stoped:
                if (Input.anyKey)
                    state = PlayerStates.Accelerating;
                break;
            // Si está acelerando se incrementa la velocidad
            case PlayerStates.Accelerating:
                movementSpeed += acceleration * Time.deltaTime;
                controller.SimpleMove(transform.forward * movementSpeed);
                OnMoving?.Invoke();
                break;
            // Si está en máxima velodicad se mueve a máxima velocidad
            case PlayerStates.MaxSpeed:
                controller.SimpleMove(transform.forward * movementSpeed);
                OnMoving?.Invoke();
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
        state = PlayerStates.Accelerating;
    }

    private void OnDrawGizmos()
    {
        frontRay = new Ray(transform.position, transform.forward * frontLenght);
        leftRay = new Ray(transform.position, transform.right * -1 * leftLenght);
        rightRay = new Ray(transform.position, transform.right * rightLenght);

        Debug.DrawRay(frontRay.origin, frontRay.direction.normalized * frontLenght, Color.red, 0.1f);
        Debug.DrawRay(leftRay.origin, leftRay.direction.normalized * leftLenght, Color.blue, 0.1f);
        Debug.DrawRay(rightRay.origin, rightRay.direction.normalized * rightLenght, Color.magenta, 0.1f);
    }

}


public enum PlayerStates
{
    Stoped,
    Accelerating,
    MaxSpeed
}
