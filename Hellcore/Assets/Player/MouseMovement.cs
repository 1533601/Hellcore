using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseMovement : MonoBehaviour
{
    private PlayerInputAction playerInputAction;

    public float mouseSensitivityX = 500f;
    public float mouseSensitivityY = 300f;

    public Vector2 mouseLook;
    
    float xRotation = 0f;

    private Transform playerBody;

    void Start()
    {
        
    }

    private void Awake()
    {
        playerBody = transform.parent;

        playerInputAction = new PlayerInputAction();

        Cursor.lockState = CursorLockMode.Locked;
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
        mouseLook = playerInputAction.PlayerMovement.Look.ReadValue<Vector2>();

        float mouseX = mouseLook.x * mouseSensitivityX * Time.deltaTime;
        float mouseY = mouseLook.y * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}

//playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);


//Reset
//if(mouseX >= 1f || mouseX <= -1f)
//    mouseX = 0f;
//if(mouseY >= 1f || mouseX <= -1f)
//    mouseX = 0f;
//mouseY = 0f;
//if (timer < resetTime)
//    timer += Time.deltaTime;
//else
//{
//    mouseX = 0f;
//    mouseY = 0f;
//}

//public void Look(InputAction.CallbackContext context)
//{
//    Vector2 inputVector = context.ReadValue<Vector2>();
//    if (context.performed)
//    {
//        mouseX = inputVector.x;
//        mouseY = inputVector.y;
//    }
//    //timer = 0f;
//    ////else if (context.canceled)
//    ////{
//    ////    mouseX = 0f;
//    ////    mouseY = 0f;
//    ////}
//    //Debug.Log(mouseX);

//    //xRotation -= mouseY;
//    //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

//    //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
//    //playerBody.Rotate(Vector3.up * mouseX);
//}