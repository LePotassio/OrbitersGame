using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    // Speed of camera lock on
    public float smoothSpeed = 0.125f;

    public Vector3 offset;

    // Avoid jitter
    private void FixedUpdate()
    {
        /*Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;*/

        transform.position = target.position + offset;

        transform.rotation = target.rotation;

        // transform.LookAt(target); Don't do this, we 2d now
    }
}
