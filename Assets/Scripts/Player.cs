using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //[SerializeField] GameObject projectile;
    private Rigidbody _rigidbody;
    [SerializeField] public int health;
    [SerializeField] public int speed;
    [SerializeField] public int damagePoints;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
