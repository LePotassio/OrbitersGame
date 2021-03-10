﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adapted from Sebastian Lague https://www.youtube.com/watch?v=7axImc1sxa0
// His code is super smart! (and he is too)

[ExecuteInEditMode]
[RequireComponent (typeof (Rigidbody2D))]
public class CelestialBody : MonoBehaviour
{
    // Celestial's Size
    public float radius;

    // Intensity of gravity for surface objs
    public float surfaceGravity;

    // Velcity vector applied to celestial at run time
    public Vector3 initialVelocity;

    // Name of the body
    public string bodyName = "Unnamed";

    public Transform spriteHolder;

    //Properties for current velocity and mass - Mutators were originally private
    public Vector2 velocity { get; private set; }
    public float mass { get; private set; }
    //bool radiusIndepMass;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        velocity = initialVelocity;
    }

    // Not used anymore?
    public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
    {
        foreach(var otherBody in allBodies)
        {
            if (otherBody != this)
            {
                float sqrDist = (otherBody.rb.position - rb.position).sqrMagnitude;
                Vector2 forceDir = (otherBody.rb.position - rb.position).normalized;

                // Newtonian (Avoid Unity phys) F = F*m1*m2/(r^2)
                Vector2 acceleration = forceDir * UniverseGlobals.gravitationalConst * otherBody.mass / sqrDist;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity(Vector2 acceleration, float timeStep)
    {
        velocity += acceleration * timeStep;
    }

    public void UpdatePosition(float timeStep)
    {
        Debug.Log(rb.position + rb.velocity * timeStep);
        rb.MovePosition(rb.position + velocity * timeStep);
    }

    // Called when script loaded OR values changed in inspector...
    public void OnValidate()
    {
        // if (!radiusIndepMass)
        mass = surfaceGravity * radius * radius / UniverseGlobals.gravitationalConst;
        spriteHolder = transform.transform.GetChild(0);
        spriteHolder.localScale = Vector2.one * radius;
        gameObject.name = bodyName;
    }

    public Rigidbody2D Rigidbody2D
    {
        get
        {
            return rb;
        }
    }

    public Vector2 Position
    {
        get
        {
            return rb.position;
        }
    }
}