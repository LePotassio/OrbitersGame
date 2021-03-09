using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanets : MonoBehaviour
{
    public int sphereCount;
    public int maxRadius;
    public List<GameObject> spheres;
    GameObject planetList;
    public float sunMass;


    public float simSpeed;

    public GameObject planetPrefab;

    private void Awake()
    {
        planetList = GameObject.FindGameObjectWithTag("PlanetList");
    }

    private void Start()
    {
        spheres = CreateSpheres(sphereCount, maxRadius);
    }

    public List<GameObject> CreateSpheres(int count, int radius)
    {
        var sphs = new List<GameObject>();

        var sphereToCopy = planetPrefab;
        Rigidbody2D rb = sphereToCopy.GetComponent<Rigidbody2D>();
        // rb is already kinematic

        for (int i = 0; i < count; i++)
        {
            var sp = GameObject.Instantiate(sphereToCopy, planetList.transform);
            sp.name = "Planet " + i;
            sp.transform.position = this.transform.position + new Vector3(Random.Range(-maxRadius, maxRadius), Random.Range(-maxRadius, maxRadius), 0);
            float size = Random.Range(.1f, 2);
            sp.transform.localScale *= size;
            sp.GetComponent<Rigidbody2D>().mass = size;
            spheres.Add(sp);
            sp.GetComponentInChildren<Celestial>().rotateSpeed = Random.Range(-.1f, .1f);
        }

        initalForces();

        return spheres;
    }

    void Update()
    {
        foreach (GameObject s in spheres)
        {
            Vector3 difference = this.transform.position - s.transform.position;

            float dist = difference.magnitude;
            Vector3 gravityDirection = difference.normalized;

            // Newtonian Grav equation
            //float gravity = 6.7f * (this.transform.localScale.x * s.transform.localScale.x * 80) / (dist * dist);
            //Vector3 gravityVector = (gravityDirection * gravity);
            //s.transform.GetComponent<Rigidbody2D>().AddForce(gravityVector, ForceMode2D.Force);

            Vector3 gravityVector = (gravityDirection * 800f * Time.deltaTime * simSpeed * sunMass);
            s.transform.GetComponent<Rigidbody2D>().AddForce(gravityVector, ForceMode2D.Force);
        }
    }

    void initalForces()
    {

        int index = 0;

        foreach (GameObject s in spheres)
        {
            //Vector3 dir = Vector3.zero;

            float ranX;
            float ranY;
            float min = .2f;

            if (index % 4 == 0 || index % 4 == 1)
                ranX = Random.Range(min, 1);
            else
                ranX = Random.Range(-1, -min);

            if (index % 4 == 0 || index % 4 == 2)
                ranY = Random.Range(min, 1);
            else
                ranY = Random.Range(-1, -min);

            ranX *= simSpeed * Time.deltaTime;
            ranY *= simSpeed * Time.deltaTime;

            Vector3 dir = new Vector3(ranX, ranY, 0);
            s.transform.GetComponent<Rigidbody2D>().AddForce(dir * 2000000, ForceMode2D.Force);

            index++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            spheres.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
