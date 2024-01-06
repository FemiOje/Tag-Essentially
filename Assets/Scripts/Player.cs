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

    private void Awake()
    {
        playerInputActions = new InputActionAsset();
        playerInputActions.Enable();
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}