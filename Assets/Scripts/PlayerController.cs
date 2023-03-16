using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;
    
    [SerializeField] private GameObject target;
    [SerializeField] private float maxVelocity = 3f;
    [SerializeField] private float maxForce = 15f;
    
    private Vector3 velocity;
    private float decreaseSpeed = 0.1f;
    
    private void Awake()
    {
        _playerControls = new PlayerControls();
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    
    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Newactionmap.Move.performed -= Move;
        _playerControls.Newactionmap.Shoot.performed -= Shoot;
    }
    
    void Start()
    {
        velocity = Vector3.zero;
        
        _playerControls.Newactionmap.Move.performed += Move;
        _playerControls.Newactionmap.Shoot.performed += Shoot;
    }
    
    void Update()
    {
    //     //if (_target != null)
    //     //{
    //         var desiredVelocity = (_target.transform.position - transform.position).normalized * _maxVelocity;
    //         
    //         var steering = desiredVelocity - velocity;
    //         steering = Vector3.ClampMagnitude(steering, _maxForce);
    //         
    //         velocity = Vector3.ClampMagnitude(velocity + steering, _maxVelocity);
    //         transform.position += velocity * Time.deltaTime;
    //         transform.forward = velocity.normalized;
    //         
    //         Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
    //         Debug.DrawRay(transform.position, steering.normalized * 2, Color.red);
    //     //}
        Vector3 move = _playerControls.Newactionmap.Move.ReadValue<Vector3>();
        velocity = move * maxVelocity;
        
        //SlowDown(move);

        transform.position += velocity * Time.deltaTime;
        
        if (velocity != Vector3.zero)
            transform.forward = velocity.normalized;

    }
    
    private void Move(InputAction.CallbackContext context)
    {
        Vector3 move = _playerControls.Newactionmap.Move.ReadValue<Vector3>();
        Debug.Log(move);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        Debug.Log("Shoot");
    }
    
    private void SlowDown(Vector3 move)
    {
        if (move == Vector3.zero)
        {
            float x = Mathf.Lerp(velocity.x, 0.0f, Time.deltaTime * decreaseSpeed);
            float y = Mathf.Lerp(velocity.y, 0.0f, Time.deltaTime * decreaseSpeed);
            float z = Mathf.Lerp(velocity.z, 0.0f, Time.deltaTime * decreaseSpeed);
            velocity = new Vector3(x, y, z);
            
            if (velocity.magnitude < 0.01f) {
                velocity = Vector3.zero;
            }
        }
    }
}
