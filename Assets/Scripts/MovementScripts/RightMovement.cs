using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMovement : MonoBehaviour
{
    public float walkSpeed = 2, runSpeed = 3.5f, runAcceleration = 2, rotationSpeed = 450;
    public float jumpHeight = 8, gravityModifier = 2;
    public float jumpGrace = 0.1f;
    public bool isGrounded;

    public bool enableInput = true;
    private Animator animator;
    private CharacterController characterController;
    public float currentSpeed, ySpeed;
    private float? lastGroundedTime, jumpBtnPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enableInput == false)
        {
            return;
        }

        // Get inputs
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal") * -1;

        // Set angle of movement
        Vector3 movementAngle = new Vector3(horizontalInput, 0f, verticalInput);

        // Handle running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);

            // Acceleration
            currentSpeed += runAcceleration * Time.deltaTime;
            if (currentSpeed > runSpeed)
            {
                currentSpeed = runSpeed;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);

            // Deceleration
            if (currentSpeed > walkSpeed)
            {
                currentSpeed -= runAcceleration * Time.deltaTime;
                if (currentSpeed < walkSpeed)
                {
                    currentSpeed = walkSpeed;
                }
            }
            else
            {
                currentSpeed = walkSpeed;
            }
        }

        float magnitude = Mathf.Clamp01(movementAngle.magnitude) * currentSpeed;
        movementAngle.Normalize();

        Vector3 velocity = movementAngle * magnitude;
        velocity = AdjustVelocityToSlope(velocity);

        Debug.Log("Normal Movement speed: " + velocity);

        // Handle jumping
        ySpeed += Physics.gravity.y * gravityModifier * Time.deltaTime;

        // Handle jumping grace period
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
            ySpeed = -0.5f;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBtnPressedTime = Time.time;
        }

        // Handle animation
        if (characterController.isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            if (ySpeed > 3)
            {
                animator.SetBool("isJumping", true);
            }
        }

        if ((Time.time - lastGroundedTime) <= jumpBtnPressedTime)
        {
            if ((Time.time - jumpBtnPressedTime) <= jumpGrace)
            {
                ySpeed = jumpHeight;

                jumpBtnPressedTime = null;
                lastGroundedTime = null;
            }
        }

        velocity.y += ySpeed;

        // Move character
        characterController.Move(velocity * Time.deltaTime);

        // Set angle of character
        float horizontalRot = Input.GetAxisRaw("Vertical");
        float verticalRot = Input.GetAxisRaw("Horizontal") * -1;

        Vector3 characterAngle = new Vector3(horizontalRot, 0f, verticalRot);
        characterAngle.Normalize();

        if (characterAngle != Vector3.zero)
        {
            animator.SetBool("isWalking", true);

            Quaternion rotation = Quaternion.LookRotation(characterAngle, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if ((characterController.collisionFlags & CollisionFlags.Sides) != 0)
        {
            animator.SetBool("isPushing", true);
        }
        else
        {
            animator.SetBool("isPushing", false);
        }
    }

    private Vector3 AdjustVelocityToSlope(Vector3 velocity)
    {
        var ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 0.2f))
        {
            var slopeRotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var adjustedVelocity = slopeRotation * velocity;

            if (adjustedVelocity.y < 0)
            {
                return adjustedVelocity;
            }
        }

        return velocity;
    }
}