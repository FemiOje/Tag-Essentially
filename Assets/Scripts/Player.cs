using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputActionAsset playerInputActions;
    [SerializeField] GameObject projectile;
    [SerializeField] public int health;
    [SerializeField] public int speed;
    [SerializeField] public int damagePoints;
    private Vector3 instantiationOffset = new Vector3(0,0,2);

    private void Awake()
    {
        playerInputActions = new InputActionAsset();
        playerInputActions.Enable();
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // add offset to instantiation prefab to avoid adding unncecessary force to player
            Instantiate(projectile, transform.position + instantiationOffset, transform.rotation);
        }
    }
}