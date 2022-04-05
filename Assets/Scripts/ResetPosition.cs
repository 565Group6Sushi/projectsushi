using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    [SerializeField]
    GameObject teleportObject;

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("ResetPlane"))
        {
            this.gameObject.transform.position = teleportObject.transform.position;
        }
    }
}
