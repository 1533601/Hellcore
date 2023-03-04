using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float gravity = -20f;
    public float jumpHeight = 1.8f;


    private Rigidbody playerRigidBody;


    bool isGrounded;

    public void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
        //playerRigidBody.velocity = Vector3.up;

        //playerRigidBody.velocity += new Vector3(0f, 0f, 0f);
        ////playerRigidBody.AddForce(Vector3.up * 1000f, ForceMode.Impulse);
        Debug.Log("Jump");
        //if (isGrounded)
        //{
        //    if (playerRigidBody.velocity.y < 0)
        //    {
        //        //playerRigidBody.AddForce(Vector3.down * 2f, ForceMode.Impulse);
        //        //playerRigidBody.velocity.y = -2f;f
        //        playerRigidBody.velocity += new Vector3(0f, -2f, 0f);
        //    }
        //    //playerRigidBody.velocity.y.Set(playerRigidBody.velocity.y - 2f);
        //    playerRigidBody.velocity += new Vector3(0f, Mathf.Sqrt(jumpHeight * -2f * gravity), 0f);

        //}
        ////if (Input.GetButtonDown("Jump") && isGrounded)
        ////{
        ////    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        ////}
        ////velocity.y += gravity * Time.deltaTime;
        //playerRigidBody.velocity += new Vector3(0f, gravity * Time.deltaTime, 0f);
    }


}
