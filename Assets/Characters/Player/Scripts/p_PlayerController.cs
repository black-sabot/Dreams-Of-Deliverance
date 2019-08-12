using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;

    private Rigidbody rigidbody;
    private Vector3 movement;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Capture input
        // TODO: Build a better system for this so update doesn't get interrupted
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Simple movement
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
