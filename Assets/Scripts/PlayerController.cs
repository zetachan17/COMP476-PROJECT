using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // private PlayerInput _playerInput;
    // private InputAction _shootAction;
    //
    //
    // private void Awake()
    // {
    //     _playerInput = GetComponent<PlayerInput>();
    //     _shootAction = _playerInput.actions["Shoot"];
    //     _shootAction.ReadValue<float>();
    // }

    private PlayerControls _playerControls;
    
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
        _playerControls.Newactionmap.Move.performed += Move;
        _playerControls.Newactionmap.Shoot.performed += Shoot;
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
}
