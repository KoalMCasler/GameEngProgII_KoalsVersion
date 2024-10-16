using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Sam Robichaud 
// NSCC Truro 2024
// This work is licensed under CC BY-NC-SA 4.0 (https://creativecommons.org/licenses/by-nc-sa/4.0/)

public class InputManager : MonoBehaviour
{           
    // Script References
    [SerializeField] private PlayerLocomotionHandler playerLocomotionHandler;
    [SerializeField] private CameraManager cameraManager; // Reference to CameraManager


    [Header("Movement Inputs")]
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public Vector2 movementInput;
    public float moveAmount;

    private InputAction playerMove;
    private InputAction playerLook;
    private InputAction playerJump;
    private InputAction playerSprint;
    public KoalsVersion playerInputScript;
    public InputActionAsset inputActionAsset;

    [Header("Camera Inputs")]
    public float scrollInput; // Scroll input for camera zoom
    public Vector2 cameraInput; // Mouse input for the camera

    public bool isPauseKeyPressed = false;


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleCameraInput();
        HandlePauseKeyInput();
    }

    public void Update()
    {
        CheckInputType();
    }

    void OnDisabled()
    {
        playerInputScript.Disable();
    }

    void OnEnable()
    {
        playerInputScript = new KoalsVersion();
        playerMove = playerInputScript.Player.Move;
        playerLook = playerInputScript.Player.Look;
        playerJump = playerInputScript.Player.Jump;
        playerSprint = playerInputScript.Player.Sprint;
        playerInputScript.Enable();
    }

    private void HandleCameraInput()
    {        
            // Get mouse input for the camera
            cameraInput = playerLook.ReadValue<Vector2>();

            // Get scroll input for camera zoom
            scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // Send inputs to CameraManager
            cameraManager.zoomInput = scrollInput;
            cameraManager.cameraInput = cameraInput;        
    }

    private void HandleMovementInput()
    {
        //movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput = playerMove.ReadValue<Vector2>();
        horizontalInput = movementInput.x;
        verticalInput = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
    }

    private void HandlePauseKeyInput()
    {
        isPauseKeyPressed = Input.GetKeyDown(KeyCode.Escape); // Detect the escape key press
    }

    private void HandleSprintingInput()
    {
        if (playerSprint.IsPressed() && moveAmount > 0.5f)
        {
            playerLocomotionHandler.isSprinting = true;
        }
        else
        {
            playerLocomotionHandler.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {
        jumpInput = playerJump.IsPressed();
        if (jumpInput)
        {
            playerLocomotionHandler.HandleJump(); // Trigger jump in locomotion handler
        }
    }

    void CheckInputType()
    {
        foreach (InputDevice device in inputActionAsset.devices)
        {
            if (device is Mouse || device is Keyboard)
            {
                //Debug.Log("Mouse/Keyboard is active");
            }
            else if (device is Gamepad)
            {
                //Debug.Log("Mouse/Keyboard is active");
            }
        }   
    }  

}
