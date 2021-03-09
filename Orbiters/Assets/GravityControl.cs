using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    // Current Planet We are on!
    public List<PlanetaryGravity> gravities;

    private Rigidbody2D rb;

    public float rotationSpeed = 20;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (gravities.Count != 0)
        {
            foreach (PlanetaryGravity gravity in gravities) {
                Vector3 gravityUp = Vector3.zero;

                gravityUp = (transform.position - gravity.transform.position).normalized;

                Vector3 localUp = transform.up;

                Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

                transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

                rb.AddForce((-gravityUp * gravity.gravity) * rb.mass);
            }
        }
    }

    public Transform getBiggestPlanetByMass()
    {
        Transform result = null;
        float highestMass = 0;
        foreach (PlanetaryGravity pg in gravities)
        {
            if (pg.transform.parent.GetComponent<Rigidbody2D>().mass > highestMass)
            {
                result = pg.transform.parent.transform;
                highestMass = pg.transform.parent.GetComponent<Rigidbody2D>().mass;
            }
        }

        return result;
    }
}
