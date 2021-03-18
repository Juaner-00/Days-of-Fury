using UnityEngine;
using System.Collections;

public class SCT_TankMovement : MonoBehaviour
{

    [SerializeField] bool AlwaysMove;
    [SerializeField] float m_Speed = 8.0f;                 // How fast the tank moves forward and back.
    [SerializeField] float m_TurnSpeed = 180f;


    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_MovementInputValue;         // The current value of the movement input.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
    private AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds.
    private Animator animator;

    string animDo; //the animation used now

    private Transform TargetForTurn;
    private Vector3 TargetForTurnOld;
    private float TargetForTurnTimer;

    private bool m_dead;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        m_MovementAudio = GetComponent<AudioSource>();
    }


    private void OnEnable()
    {
        // Also reset the input values.
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {

    }


    void Start()
    {
        // The axes names are based on player number.
        m_MovementAxisName = "Vertical";
        m_TurnAxisName = "Horizontal";

        // Store the original pitch of the audio source.
        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        if (m_dead)
            return;


        if (AlwaysMove)
            m_MovementInputValue = 0;
        else
            // Store the value of both input axes.
            m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);


        // EngineAudio();

        // var dist = TargetForTurnOld - TargetForTurn.position;
        // if (dist.sqrMagnitude > 0.01) TargetForTurnTimer = 0;
        // else TargetForTurnTimer += 1;

        // TargetForTurnOld = TargetForTurn.position;
    }

    /* 
        private void EngineAudio()
        {
            // If there is no input (the tank is stationary)...
            if (Mathf.Abs(m_MovementInputValue) == 0 && Mathf.Abs(m_TurnInputValue) < 0.1f)
            {
                // ... change the clip to idling and play it.
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
            }
            else
                m_MovementAudio.pitch = 1 + Mathf.Abs(m_MovementInputValue);
        }

     */
    private void FixedUpdate()
    {
        if (m_dead)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.002f, transform.position.z);
            return;
        }

        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
    }


    private void Move()
    {
        Vector3 movement;

        if (AlwaysMove)
            movement = transform.forward * 1 * m_Speed * Time.deltaTime;
        else
            // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
            movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

        if (m_TurnInputValue == 0)
            if (m_MovementInputValue > 0)
            { //Movement of the texture of the tank caterpillar
                if (animDo != "Move")
                {
                    animDo = "Move";
                    animator.SetBool("MoveForwStart", true);
                }
            }
            else
            if (m_MovementInputValue < 0)
            { //Movement of the texture of the tank caterpillar
                if (animDo != "Move")
                {
                    animDo = "Move";
                    animator.SetBool("MoveBackStart", true);
                }
            }
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        if (m_TurnInputValue > 0)
        { //Movement of the texture of the tank caterpillar
            if (animDo != "TurnRight")
            {
                animDo = "TurnRight";

                if (m_MovementInputValue >= 0)
                    animator.SetBool("TurnRight", true);
                else
                    animator.SetBool("TurnLeft", true);
            }
        }
        else if (m_TurnInputValue < 0)
        { //Movement of the texture of the tank caterpillar
            if (animDo != "TurnLeft")
            {
                animDo = "TurnLeft";
                if (m_MovementInputValue >= 0)
                    animator.SetBool("TurnLeft", true);
                else
                    animator.SetBool("TurnRight", true);
            }
        }
    }

}
