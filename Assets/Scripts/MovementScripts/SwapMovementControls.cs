using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMovementControls : MonoBehaviour
{
	[SerializeField]
	private bool normalMovement;
    [SerializeField]
    private bool rightMovement;
    [SerializeField]
    private bool leftMovement;

    private GameObject playerObject;
    private IEnumerator coroutine;

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            playerObject = other.gameObject;
            if (normalMovement && !rightMovement && !leftMovement && playerObject.GetComponent<PlayerMovement>() == null)
            {
                if(playerObject.GetComponent<RightMovement>() != null)
                {
                    Destroy(playerObject.GetComponent<RightMovement>());
                }
                if(playerObject.GetComponent<LeftMovement>() != null)
                {
                    Destroy(playerObject.GetComponent<LeftMovement>());
                }

                coroutine = WaitToAddDefaultControls(0.5f);
                StartCoroutine(coroutine);
            }
            else if (rightMovement && !normalMovement && !leftMovement && playerObject.GetComponent<RightMovement>() == null)
            {
                if (playerObject.GetComponent<PlayerMovement>() != null)
                {
                    Destroy(playerObject.GetComponent<PlayerMovement>());
                }
                if (playerObject.GetComponent<LeftMovement>() != null)
                {
                    Destroy(playerObject.GetComponent<LeftMovement>());
                }

                coroutine = WaitToAddRightControls(0.5f);
                StartCoroutine(coroutine);
            }
            else if (leftMovement && !normalMovement && !rightMovement && playerObject.GetComponent<LeftMovement>() == null)
            {
                if (playerObject.GetComponent<PlayerMovement>() != null)
                {
                    Destroy(playerObject.GetComponent<PlayerMovement>());
                }
                if (playerObject.GetComponent<RightMovement>() != null)
                {
                    Destroy(playerObject.GetComponent<RightMovement>());
                }

                coroutine = WaitToAddLeftControls(0.5f);
                StartCoroutine(coroutine);
            }
        }
	}

    private IEnumerator WaitToAddDefaultControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerObject.AddComponent<PlayerMovement>();
    }

    private IEnumerator WaitToAddRightControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerObject.AddComponent<RightMovement>();
    }

    private IEnumerator WaitToAddLeftControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerObject.AddComponent<LeftMovement>();
    }
}