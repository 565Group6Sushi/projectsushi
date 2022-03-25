using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed = 10f;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(desiredPosition, target.position, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition; 
    }
}