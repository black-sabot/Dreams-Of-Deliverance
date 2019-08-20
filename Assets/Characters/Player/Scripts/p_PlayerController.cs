using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class p_PlayerController : MonoBehaviour
{
    // Player movement speed
    public float movementSpeed = 18f;
    public float jumpForce = 60f;

    // Component tracking
    //private Rigidbody rb;
    private CharacterController charController;
    private Animator animator;

    // Movement tracking
    private Vector3 lastIdle;
    private Vector3 movement;
    public bool isJumping = false;

    void Start()
    {
        // Get components needed at the start, no messy drag and drop
        // Minimal impact on startup
        animator = GetComponentInChildren<Animator>();
        charController = GetComponent<CharacterController>();
        //rb = GetComponent<Rigidbody>();

        // Set to 0 which is the default 'down' state for idle animation
        // so character always starts looking at camera
        lastIdle = Vector3.zero;
    }

    void Update()
    {
        // Capture all the input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        // Go store the last movement direction, for animation
        GetLastDirectionInput(movement);

        // Set animator values
        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.z);
        animator.SetFloat("lastX", lastIdle.x);
        animator.SetFloat("lastZ", lastIdle.z);

        if (charController.isGrounded)
        {
            // Check only the xz movement
            float xzMovement = new Vector3(movement.x, 0, movement.z).sqrMagnitude;
            animator.SetBool("moving", xzMovement != 0 ? true : false);
        }
        else
        {
            // Stop movement if in the air
            animator.SetBool("moving", false);
        }
    }

    void FixedUpdate()
    {
        // Apply XZ movement because we're grounded
        movement = new Vector3(movement.x, 0, movement.z).normalized * movementSpeed;

        // Gravity
        movement.y += isJumping ? -jumpForce : Physics.gravity.y;

        // Do the move
        charController.Move(movement * Time.fixedDeltaTime);

        // Reset jump
        isJumping = false;
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

    public void Death()
    {
        // Do a dead here
    }
}
