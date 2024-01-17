using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleSampleCharacterControl : MonoBehaviour
{
    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct
    }

    [SerializeField] private float sensitivity = 2.0f;
    [SerializeField] GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    private PlayerInputActions playerInputActions;
    //private Vector2 lookInput;
    private Vector3 instantiationOffset = new Vector3(0.0f, 3.0f, 0.5f);



    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        //playerActions = new PlayerInputActions();
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }

    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
        //playerInputActions.Player.Look.performed += OnLookPerformed;
        //playerInputActions.Player.Look.canceled += OnLookCanceled;
        playerInputActions.Player.Fire.performed += OnFirePerformed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
        //playerInputActions.Player.Look.performed -= OnLookPerformed;
        //playerInputActions.Player.Look.canceled -= OnLookCanceled;
        playerInputActions.Player.Fire.performed -= OnFirePerformed;
    }

    // private void OnLookPerformed(InputAction.CallbackContext context)
    // {
    //     lookInput = context.ReadValue<Vector2>();
    // }

    // private void OnLookCanceled(InputAction.CallbackContext context)
    // {
    //     lookInput = Vector2.zero;
    // }

    //private PlayerInputActions playerActions;
    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 4;

    [SerializeField] private Animator m_animator = null;
    [SerializeField] private Rigidbody m_rigidBody = null;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Direct;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;
    private bool m_jumpInput = false;

    private bool m_isGrounded;
    private Vector2 movementInput;
    float verticalInput;
    float horizontalInput;

    private List<Collider> m_collisions = new List<Collider>();
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

    private void Update()
    {
        movementInput = playerInputActions.Player.Move.ReadValue<Vector2>();
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        if (!m_jumpInput && playerInputActions.Player.Jump.triggered)
        {
            m_jumpInput = true;
        }
        // Look();
    }

    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", m_isGrounded);

        switch (m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;

            default:
                Debug.LogError("Unsupported state");
                break;
        }

        m_wasGrounded = m_isGrounded;
        m_jumpInput = false;
    }

    private void TankUpdate()
    {
        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (verticalInput < 0)
        {
            if (walk) { verticalInput *= m_backwardsWalkScale; }
            else { verticalInput *= m_backwardRunScale; }
        }
        else if (walk)
        {
            verticalInput *= m_walkScale;
        }

        m_currentV = verticalInput;
        m_currentH = horizontalInput;

        m_currentV = Mathf.Lerp(m_currentV, verticalInput, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, horizontalInput, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            verticalInput *= m_walkScale;
            horizontalInput *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, verticalInput, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, horizontalInput, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if (direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    public void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && m_jumpInput)
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
    // private void Look()
    // {
    //     float mouseX = lookInput.x * sensitivity;
    //     float mouseY = lookInput.y * sensitivity;

    //     // Apply rotation based on input
    //     transform.Rotate(Vector3.up, mouseX);
    //     transform.Rotate(Vector3.left, mouseY);

    //     // Clamp vertical rotation to prevent flipping
    //     Vector3 currentRotation = transform.eulerAngles;
    //     currentRotation.x = Mathf.Clamp(currentRotation.x, 0f, 0f);
    //     transform.eulerAngles = currentRotation;
    // }

    public void OnFirePerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Instantiate(bullet, bulletSpawnPoint.position, transform.rotation);
            Instantiate(bullet, bulletSpawnPoint.position, bullet.transform.rotation);
        }
    }
}
