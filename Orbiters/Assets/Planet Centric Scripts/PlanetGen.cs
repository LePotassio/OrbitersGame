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
            sp.name += " " + planetNum;
            sp.transform.position = this.transform.position + new Vector3(Random.Range(-maxRadius, maxRadius), Random.Range(-maxRadius, maxRadius), 0);

            CelestialBody cb = sp.GetComponent<CelestialBody>();
            float rand = Random.Range(.2f, 2f);

            cb.spriteHolder = cb.transform.GetChild(0);
            cb.spriteHolder.localScale = rand * Vector2.one * cb.radius;
            cb.radius *= rand;

            //
            Vector2 initVel = new Vector2(Random.Range(-cb.radius / 2, cb.radius / 2), Random.Range(-cb.radius / 2, cb.radius / 2));
            cb.initialVelocity = initVel;
        }

        // Could also be an add to list function later...
        sim.LateCelestialStart();
    }
}
