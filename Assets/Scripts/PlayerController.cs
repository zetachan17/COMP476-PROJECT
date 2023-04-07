using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _playerControls;
    
    [SerializeField] private float maxVelocity = 3f;
    [SerializeField] private float maxForce = 15f;
    [SerializeField] private GameObject[] listMovingTarget; // (0 = forward) (1 = left) (2 = right) (3 = up) (4 = down)
    private Vector3 velocity;
    private float decreaseSpeed = 0.01f;
    private strg_steerinAgent _steringAgent;
    private Vector3[] offAngleList;// ( 0 = up left ) ( 1 = up right ) (2 = down left) (3 = down right)

    private Animator _animator;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _steringAgent = GetComponent<strg_steerinAgent>();
        offAngleList = new Vector3[4];
        offAngleList[0] = new Vector3(-1, 1, 0);
        offAngleList[1] = new Vector3(1, 1, 0);
        offAngleList[2] = new Vector3(-1, -1, 0);
        offAngleList[3] = new Vector3(1, -1, 0);
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    
    // unsubscribe from input events
    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Newactionmap.Move.performed -= Move;
        _playerControls.Newactionmap.Shoot.performed -= Shoot;
        _playerControls.Newactionmap.rotate.performed -= Rotate;
    }
    
    // subscribe to input events
    void Start()
    {
        velocity = Vector3.zero;
        
        _playerControls.Newactionmap.Move.performed += Move;
        _playerControls.Newactionmap.Shoot.performed += Shoot;
        _playerControls.Newactionmap.rotate.performed += Rotate;
    }
    
    void Update()
    {
      /*  Vector3 move = _playerControls.Newactionmap.Move.ReadValue<Vector3>();
        
        var desiredVelocity = move * maxVelocity;
        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        velocity = Vector3.ClampMagnitude(velocity + steering, maxVelocity);
        
        // TODO: Slow down when no input
        //SlowDown(move);

        transform.position += velocity * Time.deltaTime;
        
        if (velocity != Vector3.zero)
            transform.forward = velocity.normalized;
        
        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, steering.normalized * 2, Color.red);
      
       */
    }
    
    private void Move(InputAction.CallbackContext context)
    {
        Vector3 move = _playerControls.Newactionmap.Move.ReadValue<Vector3>();

        //affect the correct target to the coresponding vector3 value;
        if(move == Vector3.zero)
        {
            _steringAgent.targetMoveToward = listMovingTarget[0];
            _animator.SetInteger("turn", 0);
        }
        else if(move == Vector3.left)
        {
            _steringAgent.targetMoveToward = listMovingTarget[1];
            _animator.SetInteger("turn", 1);
        }
        else if(move == Vector3.right)
        {
            _steringAgent.targetMoveToward = listMovingTarget[2];
            _animator.SetInteger("turn", -1);
        }
        else if(move == Vector3.up)
        {
            _steringAgent.targetMoveToward = listMovingTarget[3];
        }
        else if(move == Vector3.down)
        {
            _steringAgent.targetMoveToward = listMovingTarget[4];
        }
        else if (move == offAngleList[0])
        {
           
            _steringAgent.targetMoveToward = listMovingTarget[5];
        }
        else if (move == offAngleList[1])
        {
            _steringAgent.targetMoveToward = listMovingTarget[6];
        }
        else if (move == offAngleList[2])
        {
            _steringAgent.targetMoveToward = listMovingTarget[7];
        }
        else if (move == offAngleList[3])
        {
            _steringAgent.targetMoveToward = listMovingTarget[8];
        }
        Debug.Log(move);
    }

    public void Rotate(InputAction.CallbackContext context)
    {
        Vector2 directionRotation = _playerControls.Newactionmap.rotate.ReadValue<Vector2>();
        Debug.Log(directionRotation);
        _steringAgent.setRotationAxis(directionRotation.x*-1);
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
