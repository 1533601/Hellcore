using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.HID;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    //public float testt = 0f;


    public CharacterController controller;

    public Camera mainCamera;

    public float maxHealth = 150f;
    public float playerHealth;
    public float playerHeight = 1f;

    //Energy
    private float maxEnergy = 150f;
    public float playerEnergy;
    [SerializeField, Range(0f, 1f)] private float energyLoss = 0.015f;
    [SerializeField, Range(0f, 1f)] private float energyGain = 0.025f;
    private float slideEnergy = 30f;
    //Timer
    [SerializeField, Range(1f, 5f)]private float energyTimer = 1.75f;
    private float Etimer = 0f;


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
    private float groundDistance = 0.6f;
    public Transform groundCheck;
    public Transform roofCheck;
    public LayerMask collisionMask;
    //Timer
    private float jumpTimer = 0.25f;
    private float Jtimer = 0f;

    private float crouchSpeed = 5f;
    [SerializeField] bool crouchButtonPressed = false;
    [SerializeField] bool isCrouched = false;
    bool jumpButtonPressed = false;
    public bool isGrounded;


    private MouseMovement mouseScript;
    private float tempMouseX;
    private float tempMouseY;

    private PlayerInputAction playerInputAction;

    //public string runButton = "left ctrl"; 
    //public string runButton = "Fire1";
    //public string crouchButton = "";

    Vector2 inputVector;
    Vector3 velocity;

    bool runButtonPressed;

    public bool aiming;



    //Vector3 lastPosition;


    //Slope Fields
    RaycastHit sloppeHit;
    Vector3 slopeMoveDirection;
    
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out sloppeHit, playerHeight / 2 + 0.5f))
        {
            Debug.Log("Test");
            if (sloppeHit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;
    }

    //public bool movementButtonPressed = false;

    //float x;
    //float y;

    private void Start()
    {
        //mainCamera = GetComponent<Camera>();
        //mouseScript = GetComponent<MouseMovement>();

        playerEnergy = maxEnergy;
        //lastEnergy = playerEnergy;
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, collisionMask);
        playerHealth = maxHealth;
    }

    private void Awake()
    {
        //playerControls = GetComponent<PlayerInput>();
        //mouseScript = new MouseMovement();
        mouseScript = mainCamera.GetComponent<MouseMovement>();
        tempMouseX = mouseScript.mouseSensitivityX;
        tempMouseY = mouseScript.mouseSensitivityY;

        playerInputAction = new PlayerInputAction();
        //Jump

        playerInputAction.PlayerMovement.Jump.performed += Jump;
        playerInputAction.PlayerMovement.Run.performed += Run;
        playerInputAction.PlayerMovement.Crouch.performed += Crouch;
        playerInputAction.PlayerMovement.Aim.performed += Aim;
        playerInputAction.PlayerMovement.Aim.canceled += Aim;
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

    void FixedUpdate()
    {

        bool isTouchingRoof = Physics.CheckSphere(roofCheck.position, groundDistance, collisionMask);
        if (isTouchingRoof)
            velocity.y += gravity * 4 * Time.deltaTime;

        //if(lastPosition != transform.position)
        //{
        //    mainCamera.transform.position += new Vector3(0f,0f,0f);
        //}
        //lastPosition = transform.position;

        if (aiming && isGrounded && !isCrouched && !crouchButtonPressed)
        {

            speed = crouchSpeed;
        }

        //Energy
        if (playerEnergy < maxEnergy)
        {
            if (Etimer < energyTimer)
            {
                Etimer += Time.deltaTime;
            }
            else
            {
                playerEnergy += energyGain;
            }
        }


        //Jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, collisionMask);
        //if (isGrounded && jumpButtonPressed)
        //    isGrounded = false;
        if (jumpButtonPressed && !crouchButtonPressed && isGrounded)
        {
            if (isCrouched)
            {
                speed = walkSpeed;
                CrouchPlayer();
            }

            //if (isGrounded)
            //{
            
            if (velocity.y < 0)
                velocity.y = -2f;

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            //}
            //else
            //{
            //    jumpButtonPressed = false;
            //}
            //if(!isGrounded)
            //{
            //    sheesh;
            //}

        }
        else
        {
            jumpButtonPressed = false;
        }

        if (!isGrounded)
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        if (Jtimer < jumpTimer)
        {
            Jtimer += Time.deltaTime;
        }


        //Movement
        if (runButtonPressed && !crouchButtonPressed && !aiming)
        {
            if(speed < walkSpeed)
                speed = walkSpeed;
            if (inputVector.y > 0f && playerEnergy > 0)
            {
                //Si le joueur est crouch, le lever
                if (isCrouched)
                {
                    CrouchPlayer();
                }
                if (speed < runSpeed)
                {
                    speed += playerSpeed;
                }
                Etimer = 0;
                playerEnergy -= energyLoss;
            }
            else//0.245
            {
                //if (speed > walkSpeed)
                //    speed -= playerSpeed;
                //else
                speed = walkSpeed;
                runButtonPressed = false;
            }
        }
        if(!crouchButtonPressed && !OnSlope())
        {
            Vector3 moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }


        //Crouch
        if (crouchButtonPressed && isGrounded)
        {
            //if(isGrounded)
            //{
            if (runButtonPressed && speed >= walkSpeed + ((runSpeed - walkSpeed) / 2) && playerEnergy >= slideEnergy)
            {
                //Si le joueur n'est pas déjà crouch (jump)
                if (!isCrouched)
                {
                    CrouchPlayer();
                }
                speed = slideSpeed;
                //Vector3 moveSlide = transform.forward;
                controller.Move(transform.forward * speed * Time.deltaTime);

                
                if (timer < slideTimer)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    playerEnergy -= slideEnergy;
                    speed = crouchSpeed;
                    timer = 0f;
                    runButtonPressed = false;
                    crouchButtonPressed = false;
                    //CrouchPlayer();
                }

            }
            else
            {
                
                if(!runButtonPressed)
                {
                    if(speed == walkSpeed)
                        speed = crouchSpeed;
                    else if(!aiming)
                        speed = walkSpeed;
                }
                else
                    speed = crouchSpeed;

                //Crouch animation here
                //CrouchPlayer();
                runButtonPressed = false;
                crouchButtonPressed = false;
            }
            //}
        }


        //Slope
        if(isGrounded && OnSlope())
        {
            Vector3 moveDirection = transform.right * inputVector.x + transform.forward * inputVector.y;
            slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, sloppeHit.normal);
            controller.Move(slopeMoveDirection.normalized * speed * Time.deltaTime);
        }
        
        
        //Debug.Log("Test");
    }


    public void Jump(InputAction.CallbackContext context)
    {

        if(context.performed && isGrounded && Jtimer >= jumpTimer)
        {
            //JumpTimer();
            Jtimer = 0f;
            //isGrounded = false;
            jumpButtonPressed = true;
        }
            
    }
    public void Run(InputAction.CallbackContext context)
    {
        if(context.performed && playerEnergy > 0)
            runButtonPressed = true;
    }
    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed && !crouchButtonPressed) 
        {
            if(isGrounded)
            {
                crouchButtonPressed = true;
                CrouchPlayer();
            }
        }
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

    public void Aim(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            mouseScript.mouseSensitivityX /= 3;
            mouseScript.mouseSensitivityY /= 3;
            aiming = true;
            //if (!isCrouched && !crouchButtonPressed)
            //    speed = crouchSpeed;

        }
            
        else if (context.canceled)
        {
            mouseScript.mouseSensitivityX = tempMouseX;
            mouseScript.mouseSensitivityY = tempMouseY;
            aiming = false;
            if(!isCrouched && !crouchButtonPressed)
                speed = walkSpeed;
        }
            
    }

    public void CrouchPlayer()
    {
        Vector3 camPos = mainCamera.transform.position;
        //if(isGrounded)
        //{
        if (isCrouched)
        {
            camPos.y = camPos.y + 0.5f;
            mainCamera.transform.position = camPos;
            isCrouched = false;
        }
        else
        {
            //speed = crouchSpeed;
            camPos.y = camPos.y - 0.5f;
            mainCamera.transform.position = camPos;
            isCrouched = true;
        }
        //}
        
    }

}
