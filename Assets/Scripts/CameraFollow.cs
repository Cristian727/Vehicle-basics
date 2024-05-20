using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetTransform;

    private Rigidbody rb;
    [SerializeField] Vector3 offset;
    Vector3 currentVelocity = Vector3.zero;
    [SerializeField] float lookAheadDistance;
    [SerializeField] float lookAheadSmooth;
    Vector3 lookAhead;
    Vector3 lookAheadVelocity = Vector3.zero;
    [SerializeField] float smoothTime = 0.1f;

    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        rb = targetTransform.gameObject.GetComponent<Rigidbody>();
    }


    void Update()
    {

    }
    void FixedUpdate()
    {


        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetTransform.position - targetTransform.forward * 10 + targetTransform.up * 7,
            ref currentVelocity,
            smoothTime
        );

        transform.LookAt(targetTransform);
    }
}