﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_PlayerController : MonoBehaviour
{
    // Player movement speed
    // TODO: add sprint?
    public float movementSpeed = 7f;
    public float rollSpeed;
    private float rollTime;
    public float startRollTime;
    private int direction;
    private bool isRolling = false;

    // Component tracking
    private Rigidbody rigidbody;
    private Animator animator;

    // Movement tracking
    private Vector3 lastIdle;
    private Vector3 movement;

    void Start()
    {
        // Get components needed at the start, no messy drag and drop
        // Minimal impact on startup
        animator = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();

        // Set to 0 which is the default 'down' state for idle animation
        // so character always starts looking at camera
        lastIdle = Vector3.zero;
        rollTime = startRollTime;
    }

    void Update()
    {
        // Capture input
        // TODO: Build a better system for this so update doesn't get interrupted
        if (!isRolling)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRolling)
        {
            isRolling = true;
            Debug.Log("Rolling away");
        }
        
        if(isRolling)
        {
            if (rollTime <= 0)
            {
                rollTime = startRollTime;
                isRolling = false;
            }
            else
            {
                rollTime -= Time.deltaTime;

                rigidbody.velocity = rigidbody.velocity * rollSpeed;
            }
        }


        // Go store the last movement direction, normalized, for animation
        GetLastDirectionInput(movement);

        // Set animator values
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.z);
        animator.SetFloat("lastX", lastIdle.x);
        animator.SetFloat("lastZ", lastIdle.z);
        animator.SetBool("moving", movement.sqrMagnitude != 0 ? true : false);
    }

    void FixedUpdate()
    {
        // Simple movement -- normalized for diagonal speed
        if (isRolling)
            if (movement == Vector3.zero)
                rigidbody.MovePosition(rigidbody.position - lastIdle.normalized * rollSpeed/2 * Time.fixedDeltaTime);
            else
                rigidbody.MovePosition(rigidbody.position + lastIdle.normalized * rollSpeed * Time.fixedDeltaTime);
        else
            rigidbody.MovePosition(rigidbody.position + movement.normalized * movementSpeed * Time.fixedDeltaTime);
    }

    void GetLastDirectionInput(Vector3 movementInput)
    {
        // Left
        if (movementInput.x < 0)
        {
            lastIdle.x = -1;
        }

        // Right
        if (movementInput.x > 0)
        {
            lastIdle.x = 1;
        }

        // Up
        if (movementInput.z < 0)
        {
            lastIdle.z = -1;
        }

        // Down
        if (movementInput.z > 0)
        {
            lastIdle.z = 1;
        }

        // Clear z if only going left or right
        if (movementInput.x != 0 && movementInput.z == 0)
        {
            lastIdle.z = 0;
        }

        // Clear x if only going up or down
        if (movementInput.x == 0 && movementInput.z != 0)
        {
            lastIdle.x = 0;
        }
    }
}
