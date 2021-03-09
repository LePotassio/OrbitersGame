using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celestial : MonoBehaviour
{
    // Name of the celestial body
    public string bodyName;

    // Intensity of gravity on players
    public float humanGravity;

    // Negative for other direction
    public float rotateSpeed;

    private void FixedUpdate()
    {
        this.gameObject.transform.Rotate(new Vector3(0, 0, 1 * rotateSpeed));
    }
}
