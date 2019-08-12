using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_PlayerController : MonoBehaviour
{
    public float movementSpeed = 7f;

    private Rigidbody rigidbody;
    private Vector3 movement;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Capture input
        // TODO: Build a better system for this so update doesn't get interrupted
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        // Set animator values
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.z);
        animator.SetFloat("speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        // Simple movement
        rigidbody.MovePosition(rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
