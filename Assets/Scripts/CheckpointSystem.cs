using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    [SerializeField]
    GameObject currentCheckpoint;

    [SerializeField]
    List<GameObject> checkpointAreas;

    [SerializeField]
    List<GameObject> checkpointPositions;

    private void Update()
    {
        if (gameObject.GetComponent<PlayerHealth>().isDead)
        {
            this.gameObject.transform.position = currentCheckpoint.transform.position;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("ResetPlane"))
        {
            this.gameObject.transform.position = currentCheckpoint.transform.position;
        }

        if (col.CompareTag("Checkpoint"))
        {
            if (checkpointAreas.Contains(col.gameObject))
            {
                currentCheckpoint = col.gameObject;

                var objectIndex = checkpointAreas.IndexOf(col.gameObject);

                if (objectIndex != -1)
                {
                    currentCheckpoint = checkpointPositions[objectIndex];

                    checkpointAreas.Remove(col.gameObject);
                    Destroy(col.gameObject);
                }
            }
        }
    }
}
