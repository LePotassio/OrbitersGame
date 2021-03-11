﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CelestialBody referenceBody;
    Rigidbody2D rb;
    Vector2 smoothVelocity;

    public float mass = 70;

    private void Awake()
    {
        InitRigidBody2D();
    }

    void InitRigidBody2D()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.gravityScale = 0;
        rb.isKinematic = false;
        rb.mass = mass;
    }

    private void FixedUpdate()
    {
        List<CelestialBody> bodies = BodySimulationEngine.Bodies;
        Vector3 strongestGrav = Vector3.zero;

        foreach (CelestialBody body in bodies)
        {
            float sqrDst = (body.Position - rb.position).sqrMagnitude;
            Vector2 forceDir = (body.Position - rb.position).normalized;
            Vector2 acceleration = forceDir * UniverseGlobals.gravitationalConst * body.mass / sqrDst;
            //rb.AddForce(acceleration, ForceMode2D.Force);

            // Find body with strongest grav pull
            if (acceleration.sqrMagnitude > strongestGrav.sqrMagnitude)
            {
                strongestGrav = acceleration;
                referenceBody = body;
            }
        }

        // Rotate to align with gravity up
        Vector2 gravityUp = -strongestGrav.normalized;
        rb.rotation = Quaternion.FromToRotation(transform.up, gravityUp).z * rb.rotation;

        rb.MovePosition(rb.position + smoothVelocity * Time.fixedDeltaTime);
    }
}
