using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySimulationEngine : MonoBehaviour
{
    List<CelestialBody> bodies;
    static BodySimulationEngine instance;

    private void Awake()
    {
        bodies = new List<CelestialBody>(FindObjectsOfType<CelestialBody>());
        Time.fixedDeltaTime = UniverseGlobals.physicsTimeStep;
        Debug.Log("Simulation engine has set fixedDeltaTime to: " + UniverseGlobals.physicsTimeStep);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < bodies.Count; i++)
        {
            Vector2 acceleration = CalculateAcceleration(bodies[i].Position, bodies[i]);
            bodies[i].UpdateVelocity(acceleration, UniverseGlobals.physicsTimeStep);
        }

        for (int i = 0; i < bodies.Count; i++)
        {
            bodies[i].UpdatePosition(UniverseGlobals.physicsTimeStep);
        }
    }

    public void LateCelestialStart()
    {
        bodies = new List<CelestialBody>(FindObjectsOfType<CelestialBody>());
    }

    public static Vector2 CalculateAcceleration(Vector2 point, CelestialBody ignoreBody = null)
    {
        Vector2 acceleration = Vector2.zero;
        foreach(var body in Instance.bodies)
        {
            if (body != ignoreBody)
            {
                float sqrDst = (body.Position - point).sqrMagnitude;
                Vector2 forceDir = (body.Position - point).normalized;
                
                if (sqrDst != 0)
                    acceleration += forceDir * UniverseGlobals.gravitationalConst * body.mass / sqrDst;
            }
        }

        return acceleration;
    }

    public static List<CelestialBody> Bodies
    {
        get
        {
            if (Instance == null)
                return null;
            return Instance.bodies;
        }
    }

    static BodySimulationEngine Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BodySimulationEngine>();
            }
            return instance;
        }
    }
}
