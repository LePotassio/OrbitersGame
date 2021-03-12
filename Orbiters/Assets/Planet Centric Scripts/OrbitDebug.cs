using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrbitDebug : MonoBehaviour
{
    // How many steps into future
    public int numSteps = 1000;

    public float timeStep = 0.1f;

    // Factor in the time dilation of the sim or not, NOTE THAT NON DEFAULT TIME STEP WILL CAUSE INNACURACY IN TIGHT ORBITS -> Best used as a long range orbit prediction
    public bool usePhysicsTimeStep;


    public bool relativeToBody;
    public CelestialBody centralBody;
    public float width = 100;
    public bool useThickLines;

    private void Start()
    {
        if (Application.isPlaying)
        {
            HideOrbits();
        }
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            DrawOrbits();
        }
    }

    void DrawOrbits()
    {
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody>();
        var virtualBodies = new VirtualBody[bodies.Length];
        var drawPoints = new Vector2[bodies.Length][];
        int referenceFrameIndex = 0;
        Vector2 referenceBodyInitialPosition = Vector2.zero;

        // Init virtual bodies so we don't move the bodies themselves
        for (int i = 0; i < virtualBodies.Length; i++)
        {
            virtualBodies[i] = new VirtualBody(bodies[i], timeStep);
            drawPoints[i] = new Vector2[numSteps + 1];

            if (bodies[i] == centralBody && relativeToBody)
            {
                referenceFrameIndex = i;
                referenceBodyInitialPosition = virtualBodies[i].position;
            }
        }

        // Generate lines through sim of steps...
        for (int step = 1; step < numSteps + 1; step++)
        {
            Vector2 referenceBodyPosition = (relativeToBody) ? virtualBodies[referenceFrameIndex].position : Vector2.zero;
            // Update velocities
            if (step >= 0) // Bug detected, need to generate first update without
            {
                for (int i = 0; i < virtualBodies.Length; i++)
                {
                    virtualBodies[i].velocity += CalculateAcceleration(i, virtualBodies) * timeStep;
                }
                // Update positions
                for (int i = 0; i < virtualBodies.Length; i++)
                {
                    // Debug.Log(i + ": " + virtualBodies[i].position);
                    Vector2 newPos = virtualBodies[i].position + virtualBodies[i].velocity * timeStep;
                    virtualBodies[i].position = newPos;
                    if (relativeToBody)
                    {
                        var referenceFrameOffset = referenceBodyPosition - referenceBodyInitialPosition;
                        newPos -= referenceFrameOffset;
                    }
                    if (relativeToBody && i == referenceFrameIndex)
                    {
                        newPos = referenceBodyInitialPosition;
                    }

                    drawPoints[i][step] = newPos;
                }
            }

            for (int i = 0; i < virtualBodies.Length; i++)
            {
                drawPoints[i][0] = bodies[i].Position;
            }
        }

        var drawPoints3 = new Vector3[bodies.Length][];
        drawPoints3 = Vector2to3(drawPoints, drawPoints3);

        // Draw paths on generated trajectories
        for (int bodyIndex = 0; bodyIndex < virtualBodies.Length; bodyIndex++)
        {
            var pathColour = bodies[bodyIndex].gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            //Debug.Log(pathColour);

            if (useThickLines)
            {
                var lineRenderer = bodies[bodyIndex].gameObject.GetComponentInChildren<LineRenderer>();
                lineRenderer.enabled = true;
                lineRenderer.positionCount = drawPoints[bodyIndex].Length;
                lineRenderer.SetPositions(drawPoints3[bodyIndex]);
                lineRenderer.startColor = pathColour;
                lineRenderer.endColor = pathColour;
                lineRenderer.widthMultiplier = width;
            }
            else
            {
                for (int i = 0; i < drawPoints[bodyIndex].Length - 1; i++)
                {
                    Debug.DrawLine(drawPoints[bodyIndex][i], drawPoints[bodyIndex][i + 1]);
                }

                // Hide renderer
                var lineRenderer = bodies[bodyIndex].gameObject.GetComponentInChildren<LineRenderer>();
                if (lineRenderer)
                {
                    lineRenderer.enabled = false;
                }
            }
        }
    }

    Vector2 CalculateAcceleration(int i, VirtualBody[] virtualBodies)
    {
        Vector3 acceleration = Vector3.zero;
        for (int j = 0; j < virtualBodies.Length; j++)
        {
            if (i == j)
            {
                continue;
            }
            Vector3 forceDir = (virtualBodies[j].position - virtualBodies[i].position).normalized;
            float sqrDst = (virtualBodies[j].position - virtualBodies[i].position).sqrMagnitude;
            if (sqrDst != 0)
                acceleration += forceDir * UniverseGlobals.gravitationalConst * virtualBodies[j].mass / sqrDst;
        }

        return acceleration;
    }

    void HideOrbits()
    {
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody>();

        // Draw paths
        for (int bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++)
        {
            var lineRenderer = bodies[bodyIndex].gameObject.GetComponentInChildren<LineRenderer>();
            lineRenderer.positionCount = 0;
        }
    }

    private void OnValidate()
    {
        if (usePhysicsTimeStep)
        {
            timeStep = UniverseGlobals.gravitationalConst;
        }
    }

    class VirtualBody
    {
        public Vector2 position;
        public Vector2 velocity;
        public float mass;

        public VirtualBody(CelestialBody body, float timeStep)
        {
            position = body.transform.position;
            velocity = body.initialVelocity;
            mass = body.mass;
        }
    }

    private Vector3[][] Vector2to3(Vector2[][] drawPoints, Vector3[][] drawPoints3)
    {
        int i = 0;
        int j = 0;
        foreach (Vector2[] vecArr in drawPoints)
        {
            j = 0;
            drawPoints3[i] = new Vector3[numSteps + 1];
            foreach (Vector2 vec in vecArr)
            {
                drawPoints3[i][j] = new Vector3(drawPoints[i][j].x, drawPoints[i][j].y, 0);
                j++;
            }
            i++;
        }

        return drawPoints3;
    }
}
