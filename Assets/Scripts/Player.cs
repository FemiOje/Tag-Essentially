using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputActionAsset playerInputActions;
    [SerializeField] GameObject projectile;
    [SerializeField] public int health;
    [SerializeField] public int speed;
    [SerializeField] public int damagePoints;
    private Vector3 instantiationOffset = new Vector3(0.0f, 0.0f, 0.5f);

    private void Awake()
    {
        playerInputActions = new InputActionAsset();
        playerInputActions.Enable();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Calculate the forward direction of the player
            Vector3 playerForward = transform.forward;

            // Adjust the instantiation offset based on player's forward direction
            Vector3 adjustedOffset = playerForward * instantiationOffset.z;

            // Instantiate the projectile with the adjusted offset
            Instantiate(projectile, transform.position + adjustedOffset, transform.rotation);
        }
    }
}
