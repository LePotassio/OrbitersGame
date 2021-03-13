using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    CelestialBody referenceBody;
    Rigidbody2D rb;
    Vector2 smoothVelocity;

    public float thrusterStrength;

    public float mass;

    Vector2 thrusterInput;

    int rotationInput;

    private void Awake()
    {
        InitRigidBody2D();
    }

    private void Update()
    {
        HandleMovement();
    }

    void InitRigidBody2D()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.gravityScale = 0;
        rb.isKinematic = false;
        rb.mass = mass;
    }

    void HandleMovement()
    {
        /*
        if (rb.angularVelocity > 50)
        {
            rb.angularVelocity = 50;
        }
        if (rb.angularVelocity < -50)
        {
            rb.angularVelocity = -50;
        }
        */


        int thrustInputX = GetInputAxis(KeyCode.A, KeyCode.D);
        int thrustInputY = GetInputAxis(KeyCode.S, KeyCode.W);
        rotationInput = GetInputAxis(KeyCode.E, KeyCode.Q);
        thrusterInput = new Vector2(thrustInputX, thrustInputY);

    }

    private void FixedUpdate()
    {
        /*
        List<CelestialBody> bodies = BodySimulationEngine.Bodies;
        Vector3 strongestGrav = Vector3.zero;

        foreach (CelestialBody body in bodies)
        {
            // Distance away from body
            float sqrDst = (body.Position - rb.position).sqrMagnitude;
            // Unit vector with direction
            Vector2 forceDir = (body.Position - rb.position).normalized; // aka gravity up
            Vector2 acceleration = forceDir * UniverseGlobals.gravitationalConst * body.mass / sqrDst;
            // rb.AddForce(acceleration, ForceMode2D.Force);
            rb.AddForce(forceDir * body.surfaceGravity * 60 * rb.mass);
            //Debug.Log("Force added to player: " + acceleration);

            //rb.MovePosition();

            // Find body with strongest grav pull
            if (acceleration.sqrMagnitude > strongestGrav.sqrMagnitude)
            {
                strongestGrav = acceleration;
                referenceBody = body;
            }
        }

        // Rotate to align with gravity up
        Vector2 gravityUp = -strongestGrav.normalized;
        if (!inShip)
            rb.rotation = Quaternion.FromToRotation(transform.up, gravityUp).z * rb.rotation;
        */    

        // Playermovement
        //rb.MovePosition(rb.position + smoothVelocity * Time.fixedDeltaTime);


        Vector2 gravity = BodySimulationEngine.CalculateAcceleration(rb.position);
        rb.AddForce(gravity, ForceMode2D.Impulse);

        // Debug.Log("Adding force: " + transform.TransformVector(thrusterInput));
        rb.AddForce(transform.TransformVector(thrusterInput) * rb.mass * thrusterStrength, ForceMode2D.Impulse);
        //rb.angularVelocity += rotationInput * 2;
        rb.rotation += rotationInput * 2;
    }

    int GetInputAxis(KeyCode negativeAxis, KeyCode positiveAxis)
    {
        int axis = 0;
        if (Input.GetKey(positiveAxis))
        {
            axis++;
        }
        if (Input.GetKey(negativeAxis))
        {
            axis--;
        }

        return axis;
    }
}
