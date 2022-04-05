using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject door;

    public bool isTriggered = false;
    public double maxHeight = 3.0, transformRate = 0.1;
    public double currentMovement = 0;

    private void OnTriggerEnter(Collider col)
    {
        if (!isTriggered)
        {
            if (col.CompareTag("TriggerCube"))
            {
                isTriggered = true;
            }
        }
    }

    void Update()
    {
        if (currentMovement < maxHeight && isTriggered)
        {
            currentMovement += transformRate;
            door.transform.position += new Vector3(0, (float) transformRate, 0);
        }
    }
}
