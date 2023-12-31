using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Rigidbody playerRb;
    [SerializeField] private float moveSpeed;
    private PlayerInputActions playerActions;
    [SerializeField] private float jumpForce;
    private Vector2 movementInput;

    private void Awake()
    {
        playerActions = new PlayerInputActions();
        playerRb = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material.SetColor("_Color", Random.ColorHSV());
        }
    }

    private void OnEnable()
    {
        playerActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerActions.Player.Disable();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MovePlayer()
    {
        // Move the player
        movementInput = playerActions.Player.Move.ReadValue<Vector2>();
        playerRb.velocity = new Vector3(movementInput.x, 0, movementInput.y) * moveSpeed;
    }
}
