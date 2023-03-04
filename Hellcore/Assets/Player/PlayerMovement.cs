using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float maxHealth = 150f;
    public float playerHealth;

    //private PlayerInput playerControls;

    public float slideSpeed = 16f;
    public float slideTimer = 0.5f;
    public float timer = 0f;

    public float playerSpeed = 0.05f;
    public float runSpeed = 11f;
    public float walkSpeed = 7f;

    public float speed = 7f;

    //Jump Field
    public float gravity = -50f;
    public float jumpHeight = 1.8f;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    bool crouchButtonPressed = false;
    bool jumpButtonPressed = false;
    bool isGrounded;


    private PlayerInputAction playerInputAction;

    //public string runButton = "left ctrl"; 
    //public string runButton = "Fire1";
    //public string crouchButton = "";

    Vector2 inputVector;
    Vector3 velocity;

    
    bool runButtonPressed;
    //public bool movementButtonPressed = false;

    //float x;
    //float y;


    private void Awake()
    {
        //playerControls = GetComponent<PlayerInput>();

        playerInputAction = new PlayerInputAction();
        //Jump

        playerInputAction.PlayerMovement.Jump.performed += Jump;
        playerInputAction.PlayerMovement.Run.performed += Run;
        playerInputAction.PlayerMovement.Crouch.performed += Crouch;
        //playerInputAction.PlayerMovement.Movement.performed += Movement;

        //playerControls.onActio
        //PlayerInputActions playerInputActions = new PlayerInputActions();
        
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    void Update()
    {
        //Movement
        if (runButtonPressed && !crouchButtonPressed)
        {
            if (inputVector.y > 0f)
            {
                if (speed < runSpeed)
                {
                    speed += playerSpeed;
                }
            }
            else
            {
                //if (speed > walkSpeed)
                //    speed -= playerSpeed;
                //else
                speed = walkSpeed;
                runButtonPressed = false;
            }
        }
        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(move * speed * Time.deltaTime);


        //Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (jumpButtonPressed && !crouchButtonPressed)
        {
            if (isGrounded)
            {
                if (velocity.y < 0)
                    velocity.y = -2f;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else
            {
                jumpButtonPressed = false;
            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Crouch
        if(crouchButtonPressed)
        {
            if(isGrounded)
            {
                if (runButtonPressed && speed >= walkSpeed + ((runSpeed - walkSpeed) / 2))
                {
                    speed = slideSpeed;

                    if (timer < slideTimer)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {

                        speed = walkSpeed;
                        timer = 0f;
                        runButtonPressed = false;
                        crouchButtonPressed = false;
                    }

                }
                else
                {
                    //Crouch animation here

                    crouchButtonPressed = false;
                }
            }
        }
    }


    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && isGrounded)
            jumpButtonPressed = true;
    }
    public void Run(InputAction.CallbackContext context)
    {
        if(context.performed)
            runButtonPressed = true;
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.performed && !crouchButtonPressed)
            crouchButtonPressed = true;
    }
    public void Movement(InputAction.CallbackContext context)
    {
        if (context.performed)
            inputVector = context.ReadValue<Vector2>();
        else if (context.canceled)
        {
            inputVector.x = 0;
            inputVector.y = 0;
        }
    }


}
