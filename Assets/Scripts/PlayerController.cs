using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float speed;
    private float rotationSpeed;
    private Rigidbody rb;
    [SerializeField] Camera camera;
    private Vector2 XZAxis;

    void Start()
    {
        speed = 10;
        rotationSpeed = 10;
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        var transformCam = camera.transform;
        Vector3 movement = transformCam.right * XZAxis.x + transformCam.forward * XZAxis.y;

        Vector3 direction = new Vector3(movement.x, 0f, movement.z);
        rb.velocity = direction * speed + rb.velocity.y * Vector3.up;

        if (XZAxis.magnitude != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed);
        }
    }

    
    void OnMovement(InputValue prmInputValue)
    {
        XZAxis = prmInputValue.Get<Vector2>();
    }
}
