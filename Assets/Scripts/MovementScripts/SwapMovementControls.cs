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

	private void OnTriggerEnter(Collider other)
	{
        if (other.CompareTag("Player"))
        {
            GameObject playerObject = other.gameObject;
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

                playerObject.AddComponent<PlayerMovement>();
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

                playerObject.AddComponent<RightMovement>();
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
            
                playerObject.AddComponent<LeftMovement>();
            }
        }
	}
}