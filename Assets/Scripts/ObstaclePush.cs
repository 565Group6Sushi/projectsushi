using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePush : MonoBehaviour
{
    [SerializeField]
    private float forceMagnitude = 27;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        // Check if collision object has rigid body
        if (rigidbody == null)
        {
            return;
        }

        if (hit.collider.GetType() == typeof(SphereCollider))
        {
            ApplyForce(hit);
        } else if (hit.collider.GetType() == typeof(BoxCollider))
        {
            if (hit.moveDirection.y < -0.3f)
            {
                return;
            }
            ApplyForce(hit);
        } else
        {
            return;
        }
    }

    private void ApplyForce(ControllerColliderHit hit)
    {
        // Calculate direction of force
        Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
        forceDirection.y = 0;
        forceDirection.Normalize();

        hit.collider.attachedRigidbody.AddForceAtPosition(forceDirection * forceMagnitude * 0.3f, transform.position, ForceMode.Impulse);
    }
}
