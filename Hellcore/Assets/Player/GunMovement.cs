using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class GunMovement : MonoBehaviour
{
    public GameObject gunHolder;

    private PlayerInputAction playerInputAction;

    //private Vector3 gunStartPosition;
    //private float gunAimPosition = 0.38f;
    //private float incDecUpdate = 0.01f;

    private float gunMovement = 0.4f;

    private float aimAnimationTimer = 0.075f;
    public float aimTimer = 0f;

    public bool aim;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();

        playerInputAction.PlayerMovement.Aim.performed += Aim;
        playerInputAction.PlayerMovement.Aim.canceled += AimCancel;
    }

    private void OnEnable()
    {
        playerInputAction.Enable();
    }

    private void OnDisable()
    {
        playerInputAction.Disable();
    }

    private void Start()
    {
    }

    void Update()
    {
        //gunStartPosition = gunHolder.transform.position;
        //float pos = gunHolder.transform.position.x + gunHolder.transform.position.y + gunHolder.transform.position.z;
        //float pos2 = pos - (gunAimPosition * 3);
        if (aim)
        {
            //gunHolder.transform.Translate(gunMovement, 0f, 0f);
            //if (aimTimer > aimAnimationTimer)
            //{
            //    aimTimer = aimAnimationTimer;
            //    gunHolder.transform.Translate(gunMovement, 0f, 0f);
            //}
            //if (aimTimer < aimAnimationTimer) 
            //{
            //    gunHolder.transform.Translate(-gunMovement, 0f, 0f);
            //    aimTimer += Time.deltaTime;
            //}

        } 
        else
        {
            //gunHolder.transform.Translate(gunMovement, 0f, 0f);
            //if (aimTimer < 0f)
            //{
            //    aimTimer = 0f;
            //    gunHolder.transform.Translate(-gunMovement, 0f, 0f);
            //}
            //if (aimTimer > 0f)
            //{
            //    gunHolder.transform.Translate(gunMovement, 0f, 0f);
            //    aimTimer -= Time.deltaTime;
            //}

        }
    }

    private void Aim(InputAction.CallbackContext context)
    {
        aim = true;
        if(aim)
            gunHolder.transform.Translate(-gunMovement, 0f, 0f);
        //if (context.performed)
        //{
        //    gunHolder.transform.Translate(-gunMovement, 0f, 0f);
        //    aim = true;
        //}
        //if (context.canceled)
        //{
        //    gunHolder.transform.Translate(gunMovement, 0f, 0f);
        //    aim = false;
        //}
    }

    private void AimCancel(InputAction.CallbackContext context)
    {
        aim = false;
        if(!aim)
            gunHolder.transform.Translate(gunMovement, 0f, 0f);
    }
}
