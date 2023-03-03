using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float playerSpeed = 0.25f;
    public float runSpeed = 16f;
    public float walkSpeed = 12f;

    public float speed;

    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //public string runButton = "left ctrl"; 
    public string runButton = "Fire1"; 


    Vector3 velocity;

    bool isGrounded;

    void Start()
    {
        
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        //if(Input.GetKey(runButton))
        if(Input.GetButton(runButton))
        {
            if (speed < runSpeed)
            {
                speed += playerSpeed;
            }
            //speed = runSpeed; 
        } 
        else
        {
            speed = walkSpeed;
        }

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
