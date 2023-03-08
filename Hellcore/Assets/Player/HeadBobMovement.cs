using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HeadBobMovement : MonoBehaviour
{
    //[SerializeField] private bool enable = true;

    //[SerializeField, Range(0, 0.1f)] private float amplitude = 0.015f;
    //[SerializeField, Range(0, 30)] private float frequency = 10.0f;

    //[SerializeField] private Transform camera = null;
    //[SerializeField] private Transform cameraHolder = null;

    //private float toggleSpeed = 3.0f;
    //private Vector3 startPos;
    //private CharacterController controller;



    //private Vector3 lastPosition;


    //private void Awake()
    //{
        
    //}

    //private void FixedUpdate()
    //{
    //    if (lastPosition != transform.position)
    //    {
    //        transform.position += new Vector3(0f, 1f, 0f);
    //    }
    //    lastPosition = transform.position;
    //}





    //void Awake()
    //{
    //    controller = GetComponent<CharacterController>();
    //    startPos = camera.localPosition;
    //}

    //private Vector3 FootStepMotion()
    //{
    //    Vector3 pos = Vector3.zero;
    //    pos.y += Mathf.Sin(Time.deltaTime * frequency) * amplitude;
    //    pos.x += Mathf.Sin(Time.deltaTime * frequency / 2) * amplitude * 2;
    //    return pos;
    //}

    //private void CheckMotion()
    //{
    //    float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;

    //    if (speed < toggleSpeed) return;
    //    if (!controller.isGrounded) return;

    //    PlayMotion(FootStepMotion());
    //}

    //private void PlayMotion(Vector3 motion)
    //{
    //    camera.localPosition += motion;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if(!enable) return;

    //    CheckMotion();
    //    ResetPosition();
    //    //camera.LookAt();
    //}

    //private void ResetPosition()
    //{
    //    if (camera.localPosition == startPos) return;
    //    camera.localPosition = Vector3.Lerp(camera.localPosition, startPos, 1 * Time.deltaTime);
    //}
}
