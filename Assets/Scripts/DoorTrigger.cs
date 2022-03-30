using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    public bool isTriggered = false;

    private void OnTriggerEnter(Collider col)
    {
        if (!isTriggered)
        {
            if (col.CompareTag("TriggerCube"))
            {
                isTriggered = true;
                door.transform.position += new Vector3(0, 2, 0);
            }
        }
    }
}
