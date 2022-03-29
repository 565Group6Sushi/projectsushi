using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Vector3 PlayerMovementInput;
	[SerializeField] private float speed, turnSpeed, movementAcceleration,
									runSpeed, jumpHeight, currentSpeed;

	[SerializeField] private Rigidbody rigidBody;

	private Animator animator;
	private Quaternion initialRot;
	private float yRot, fallMultiplier = 2.5f, lowJumpMultiplier = 2f;
	public bool isGrounded, isJump;

	//Start is called before the first frame update
	void Start()
	{
		//faces the player forward when using rotations
		initialRot = this.rigidBody.transform.rotation;

		isJump = false;

		animator = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody>();
	}

	//Update is called once per frame
	void Update()
	{
        //Player movement (forward, backward, rotate left/right)
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

		//this velocity variable is strictly for walk/run/jump animation
        var velocity = Input.GetAxis("Vertical") * currentSpeed * Vector3.forward;
        MovePlayer();

		//Player jump action
		CheckGrounded();
        if (isGrounded)
        {
			isJump = false;
        }
        //adds more weight to falling and adjusts jump height based on tapping/holding spacebar
        if (rigidBody.velocity.y < 0f)
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rigidBody.velocity.y > 0f && !Input.GetButton("Jump"))
        {
            rigidBody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
		//performs the jump
		if (isGrounded && Input.GetButtonDown("Jump"))
		{
			JumpPlayer();
		}

        //animation specifications
        animator.SetFloat("Speed", velocity.z);
		animator.SetBool("IsJump", isJump);
	}

    private void MovePlayer()
    {
		//player running
		if (Input.GetKey(KeyCode.LeftShift))
		{
			Vector3 RunVector = transform.TransformDirection(PlayerMovementInput) * runSpeed * speed;
			currentSpeed = speed * runSpeed;
			rigidBody.velocity = new Vector3(RunVector.x, rigidBody.velocity.y, RunVector.z);

			//turning with a and d keys
			yRot += PlayerMovementInput.x * turnSpeed;
			rigidBody.transform.rotation = initialRot * Quaternion.Euler(0f, yRot, 0f);
		}
		//player walking
		else
		{
			Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * speed;
			currentSpeed = speed;
			rigidBody.velocity = new Vector3(MoveVector.x, rigidBody.velocity.y, MoveVector.z);

			//turning with a and d keys
			yRot += PlayerMovementInput.x * turnSpeed;
			rigidBody.transform.rotation = initialRot * Quaternion.Euler(0f, yRot, 0f);
		}
	}

	private void JumpPlayer()
    {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			rigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
			isJump = true;
		}
	}

    void CheckGrounded()
    {
		//implementing the ground check this way adds an unreliable double jump
        isGrounded = Physics.Raycast(transform.position, -transform.up, out _, 1f);
    }
}