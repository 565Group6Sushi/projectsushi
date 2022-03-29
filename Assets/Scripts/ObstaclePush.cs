using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        // Check if collision object has rigid body
        if (rigidbody == null)
        {
            return;
        }

        // Calculate direction of force
        Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
        forceDirection.y = 0;
        forceDirection.Normalize();

        rigidbody.AddForceAtPosition(forceDirection * forceMagnitude * 0.2f, transform.position, ForceMode.Impulse);
    }
}