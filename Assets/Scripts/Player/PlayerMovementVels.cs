using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementVels : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] float maxSpeedBase;
    [SerializeField] float slowDownPercentage;
    [SerializeField] float minVelPercentage;
    [SerializeField] float acceleration;
    [SerializeField] float crashCoolDown;
    [SerializeField] Directions initialDirection;
    [SerializeField] float rotationTimeBase;
    [SerializeField] float oppositeRotationMultiplier;
    [SerializeField] AnimationCurve accelerationCurve;
    [SerializeField] float curveDuration;

    [Header("Ray Properties")]
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] float frontLenght;
    [SerializeField] float leftLenght;
    [SerializeField] float rightLenght;

    [Header("Debug")]
    [SerializeField] float movementSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float crashCoolDownTimer;
    [SerializeField] Directions lastDir;

    Directions turnDir;

    float horizontal;
    float vertical;
    float curveTimer;

    float rotationTime;

    float slowDownMultiplier;

    CharacterController controller;

    bool available;
    [SerializeField]
    bool isSlowDown;

    PlayerStates state;

    Ray frontRay;
    Ray leftRay;
    Ray rightRay;

    [Header("Rotation Angles")]
    [SerializeField] float fromAngle;
    [SerializeField] float currentAngle;
    [SerializeField] float toAngle;
    [SerializeField] float time;

    public static Action OnMoving;
    public static Action OnStoped;

    public static Action OnMovingObjetive;


    private void OnEnable()
    {
        FirstScreen.OnFirstClick += FirstClick;
    }

    private void OnDisable()
    {
        FirstScreen.OnFirstClick -= FirstClick;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        state = PlayerStates.Stopped;
        movementSpeed = 0;
        maxSpeed = maxSpeedBase;
        curveTimer = 0;
        time = 0;
        available = true;

        crashCoolDownTimer = 0;
        lastDir = initialDirection;
    }

    private void Update()
    {
        if (available && !Menu.IsPaused)
        {
            HandleInputs();
            // Solo se llama si hay un input
            HandleDirection();

            HandleNextRotation();
            HandleRotation();

            HandleRayCast();
            HandleSpeed();
            HandleGravity();


            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                OnMovingObjetive?.Invoke();
            }
        }


    }

    void HandleRayCast()
    {
        frontRay = new Ray(transform.position, transform.forward * frontLenght);
        leftRay = new Ray(transform.position, transform.right * -1 * leftLenght);
        rightRay = new Ray(transform.position, transform.right * rightLenght);

        if (!isSlowDown)
        {
            // Si no tiene el coolDown
            if (crashCoolDownTimer <= 0)
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
                            state = PlayerStates.Stopped;
                            movementSpeed = 0;
                            OnStoped?.Invoke();
                            break;
                        // Si tiene la máxima velocidad se le reduce y se pone en el estado acelerando
                        case PlayerStates.MaxSpeed:
                            state = PlayerStates.Accelerating;
                            movementSpeed *= 0.3f;
                            break;
                    }

                    crashCoolDownTimer = crashCoolDown;
                }
            }
        }

        if (state != PlayerStates.Stopped)
            crashCoolDownTimer -= Time.deltaTime;
        crashCoolDownTimer = Mathf.Clamp(crashCoolDownTimer, 0, crashCoolDown);
    }

    void HandleSpeed()
    {
        switch (state)
        {
            // Si está parado y presiona cualquier tecla se pone en acelerando
            case PlayerStates.Stopped:
                if ((horizontal != 0 || vertical != 0) && !Menu.IsPaused)
                {
                    curveTimer = 0;
                    state = PlayerStates.Accelerating;
                }
                break;
            // Si está acelerando se incrementa la velocidad
            case PlayerStates.Accelerating:
                float accelerationMagnitud = accelerationCurve.Evaluate(curveTimer / curveDuration);

                float accelerationMultiplier = isSlowDown ? 0f : 1f;
                movementSpeed += acceleration * accelerationMagnitud * Time.deltaTime * accelerationMultiplier;
                curveTimer += Time.deltaTime;
                controller.Move(transform.forward * movementSpeed * Time.deltaTime);
                OnMoving?.Invoke();

                if (movementSpeed > maxSpeed)
                {
                    movementSpeed = maxSpeed;
                    state = PlayerStates.MaxSpeed;
                }
                break;
            // Si está en máxima velodicad se mueve a máxima velocidad
            case PlayerStates.MaxSpeed:
                controller.Move(transform.forward * movementSpeed * Time.deltaTime);
                curveTimer = 0;
                OnMoving?.Invoke();
                break;
        }
    }

    void HandleInputs()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void HandleDirection()
    {
        Directions oppositeDirection = GetOpositeDirection();

        // // Obtener la dirección a girar
        // if (vertical > 0.1f)
        //     turnDir = Directions.North;
        // else if (vertical < -0.1f)
        //     turnDir = Directions.South;
        // else if (horizontal > 0.1f)
        //     turnDir = Directions.East;
        // else if (horizontal < -0.1f)
        //     turnDir = Directions.West;

        //Dirección a la que mira (4 direcciones)
        //East
        if (horizontal > 0.1f && Mathf.Abs(horizontal) >= Mathf.Abs(vertical))
            turnDir = Directions.East;
        //West
        else if (horizontal < -0.1f && Mathf.Abs(horizontal) >= Mathf.Abs(vertical))
            turnDir = Directions.West;
        //North
        else if (vertical > 0.1f && Mathf.Abs(horizontal) < Mathf.Abs(vertical))
            turnDir = Directions.North;
        //South
        else if (vertical < -0.1f && Mathf.Abs(horizontal) < Mathf.Abs(vertical))
            turnDir = Directions.South;


        // Cambiar la duración de la rotación
        if (turnDir == oppositeDirection)
            rotationTime = rotationTimeBase * oppositeRotationMultiplier;
        else
            rotationTime = rotationTimeBase;
    }

    void HandleNextRotation()
    {
        if (turnDir != lastDir)
        {
            // Girar el tanque con DoTween
            if (vertical > 0.1f)
            {
                toAngle = 0;
                // transform.DOLocalRotate(Vector3.up * 0, rotationTime, RotateMode.Fast);
                lastDir = Directions.North;
            }
            else if (vertical < -0.1f)
            {
                toAngle = 180;
                // transform.DOLocalRotate(Vector3.up * -180, rotationTime, RotateMode.Fast);
                lastDir = Directions.South;
            }
            else if (horizontal > 0.1f)
            {
                toAngle = 90;
                // transform.DOLocalRotate(Vector3.up * 90, rotationTime, RotateMode.Fast);
                lastDir = Directions.East;
            }
            else if (horizontal < -0.1f)
            {
                toAngle = -90;
                // transform.DOLocalRotate(Vector3.up * -90, rotationTime, RotateMode.Fast);
                lastDir = Directions.West;
            }

            fromAngle = transform.eulerAngles.y;
            time = 0;
        }
    }

    void HandleRotation()
    {
        if (time < rotationTime)
        {
            currentAngle = Mathf.LerpAngle(fromAngle, toAngle, time / rotationTime);
            transform.eulerAngles = Vector3.up * currentAngle;
            time += Time.deltaTime;
        }
        else
        {
            currentAngle = toAngle;
            transform.eulerAngles = Vector3.up * currentAngle;
        }
    }

    void HandleGravity()
    {
        if (!controller.isGrounded)
            controller.Move(transform.up * Physics.gravity.y * Time.deltaTime);
    }

    Directions GetOpositeDirection()
    {
        return (Directions)(((int)lastDir + 2) % 4);
    }

    void FirstClick()
    {
        available = true;
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

    public bool IsSlowDown
    {
        get => isSlowDown;
        set
        {
            isSlowDown = value;

            slowDownMultiplier = isSlowDown ? 1f - slowDownPercentage / 100f : 1f;
            movementSpeed *= slowDownMultiplier;
            movementSpeed = (movementSpeed < maxSpeed * minVelPercentage / 100f) ? maxSpeed * minVelPercentage / 100f : movementSpeed;

            state = PlayerStates.Accelerating;
        }
    }

}


public enum PlayerStates
{
    Stopped,
    Accelerating,
    MaxSpeed
}

public enum Directions
{
    North,
    East,
    South,
    West
}
