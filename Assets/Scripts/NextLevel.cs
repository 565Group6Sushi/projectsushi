using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private GameObject collidingObject;
    public GameObject blackOutSquare;
    public string levelName;

    private bool nextLevel = false;
    private double maxHeight = 0.05, transformRate = 0.012, currentMovement = 0;

    // Update is called once per frame
    void Update()
    {
        if (nextLevel)
        {
            if (collidingObject == null)
            {
                return;
            }

            if (currentMovement < maxHeight)
            {
                currentMovement += transformRate * Time.deltaTime;
                collidingObject.transform.position += new Vector3(0, (float)transformRate, 0);
                if (currentMovement > maxHeight / 2)
                {
                    StartCoroutine(FadeToBlack.FadeBlackOutSquare(blackOutSquare));
                }
            } else
            {
                nextLevel = false;
                SceneManager.LoadScene(levelName);
            }
        }
    }

        void OnTriggerEnter(Collider other)
    {
        collidingObject = other.gameObject;

        if (collidingObject.name == "SushiDude")
        {
            animator = collidingObject.GetComponent<Animator>();
            if (animator == null)
            {
                return;
            }

            characterController = collidingObject.GetComponent<CharacterController>();
            if (characterController == null)
            {
                return;
            }

            disableInput(collidingObject);
            changeAnimation();
            nextLevel = true;
        }
    }

    private void disableInput(GameObject gameObject)
    {
        PlayerMovement movementComponent = collidingObject.GetComponent<PlayerMovement>();
        movementComponent.enableInput = false;
    }

    private void changeAnimation()
    {
        animator.SetBool("isJumping", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isClimbing", true);
    }
}
