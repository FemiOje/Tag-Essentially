using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Rigidbody playerRb;
    private PlayerInputActions playerActions;
    private BoxCollider playerCollider;

    [Header("Ground Check Properties")]
    [SerializeField] private float groundCheckHeight;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float disableGCTime;
    private Vector2 boxCenter;
    private Vector2 boxSize;
    private bool isJumping;
    private float initialGravityScale = 1.0f;
    private bool groundCheckEnabled = true;
    private WaitForSeconds wait;


    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower;

    [SerializeField]
    [Range(1f, 5f)]
    private float jumpFallGravityModifier;

    private Vector2 movementInput;
    public static float globalGravity = -9.81f;
    public float gravityScale = 1.0f;

    private void Awake()
    {
        playerActions = new PlayerInputActions();

        playerRb = GetComponent<Rigidbody>();
        if (playerRb is null)
        {
            Debug.Log("Player Rigidbody is null!");
        }

        playerCollider = GetComponent<BoxCollider>();
        if (playerCollider is null)
        {
            Debug.Log("Player Collider is null!");
        }

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Random.ColorHSV());
        }

        initialGravityScale = gravityScale;
        wait = new WaitForSeconds(disableGCTime);

        playerActions.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (IsGrounded())
        {
            playerRb.velocity += Vector3.up * jumpPower;
            isJumping = true;
            StartCoroutine(EnableGroundCheckAfterJump());
        }
    }
    private bool IsGrounded()
    {
        // Use Vector3 for boxCenter
        boxCenter = new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.center.y, 0f) + (Vector3.down * (playerCollider.bounds.extents.y + (groundCheckHeight / 2f)));

        // Use Vector3 for boxSize
        boxSize = new Vector3(playerCollider.bounds.size.x, groundCheckHeight, 1f);

        var groundBox = Physics.OverlapBox(boxCenter, boxSize / 2f, Quaternion.identity, groundMask);

        if (groundBox.Length > 0)
        {
            Debug.Log("Grounded! Collider: " + groundBox[0].name);
            return true;
        }

        Debug.Log("Not Grounded!");
        return false;
    }


    private IEnumerator EnableGroundCheckAfterJump()
    {
        groundCheckEnabled = false;
        yield return wait;
        groundCheckEnabled = true;
    }

    private void HandleGravity()
    {
        if (groundCheckEnabled && IsGrounded())
        {
            isJumping = false;
        }
        else if (isJumping && playerRb.velocity.y < 0f)
        {
            gravityScale = initialGravityScale * jumpFallGravityModifier;
        }
        else
        {
            gravityScale = initialGravityScale;
        }
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
        playerRb.useGravity = false;
    }

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }
    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        playerRb.AddForce(gravity, ForceMode.Acceleration);
        
        HandleMovement();
        HandleGravity();
        Debug.Log(IsGrounded());
    }

    private void OnDrawGizmos()
    {
        if (isJumping)
        {
            Gizmos.color = Color.red;
        } else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }


    private void HandleMovement()
    {
        movementInput = playerActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed * Time.fixedDeltaTime);
    }
}
