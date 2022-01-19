using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float rotationSpeed = 10;
    [SerializeField] new Camera camera;
    private Rigidbody rb;
    private Vector2 XZAxis = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Transform transformCam = camera.transform;
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
