using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGen : MonoBehaviour
{
    public bool activated;
    public GameObject planetPrefab;

    public BodySimulationEngine sim;

    public int planetCount;
    public float maxRadius;

    void Start()
    {
        if (activated)
        {
            Generate();
        }
    }

    void Generate()
    {
        for (int planetNum = 0; planetNum < planetCount; planetNum++)
        {
            var sp = GameObject.Instantiate(planetPrefab, transform);
            sp.name = "World " + (planetNum + 1);
            sp.transform.position = this.transform.position + new Vector3(Random.Range(-maxRadius, maxRadius), Random.Range(-maxRadius, maxRadius), 0);

            CelestialBody cb = sp.GetComponent<CelestialBody>();
            // May need adjusting, baised towards bigger, could make small med large categories for easy implmentation
            float rand = 1;
            if (planetNum % 2 == 0)
                rand = Random.Range(1f, 3f);
            else
                rand = Random.Range(.1f, .4f);

            cb.spriteHolder = cb.transform.GetChild(0);
            cb.spriteHolder.localScale = rand * Vector2.one * cb.radius;
            cb.radius *= rand;
            Debug.Log(sp.name + ": " + cb.radius);
            cb.surfaceGravity += rand * 2;
            //
            Vector2 initVel = new Vector2(Random.Range(-cb.radius, cb.radius), Random.Range(-cb.radius, cb.radius));
            //Debug.Log(initVel);
            cb.velocity = initVel;
            cb.initialVelocity = initVel;
        }

        // Could also be an add to list function later...
        sim.LateCelestialStart();
    }
}
