using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5, runSpeed = 8, runAcceleration = 7, rotationSpeed = 500, jumpHeight = 10, gravityModifier = 3;

    private CharacterController characterController;
    private float currentSpeed, ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Set angle of movement
        Vector3 movementAngle = new Vector3(horizontalInput,    0f, verticalInput);

        // Handle running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Acceleration
            currentSpeed += runAcceleration * Time.deltaTime;
            if (currentSpeed > runSpeed)
            {
                currentSpeed = runSpeed;
            }
        }
        else
        {
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

        // Handle jumping
        ySpeed += Physics.gravity.y * gravityModifier * Time.deltaTime;

        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpHeight;
            }
        }
        
        velocity.y = ySpeed;

        // Move character
        characterController.Move(velocity * Time.deltaTime);

        // Set angle of character
        float horizontalRot = Input.GetAxisRaw("Horizontal");
        float verticalRot = Input.GetAxisRaw("Vertical");

        Vector3 characterAngle = new Vector3(horizontalRot, 0f, verticalRot);
        characterAngle.Normalize();

        if (characterAngle != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(characterAngle, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}
