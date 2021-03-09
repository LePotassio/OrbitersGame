using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetaryGravity : MonoBehaviour
{
    public float gravity;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<GravityControl>())
        {
            if (collision.transform.parent == null)
            {
                collision.transform.SetParent(gameObject.transform);
            }
            else if (transform.parent.GetComponent<Rigidbody2D>().mass > collision.transform.parent.parent.GetComponent<Rigidbody2D>().mass)
            {
                collision.transform.SetParent(gameObject.transform);
            }

            collision.GetComponent<GravityControl>().gravities.Add(this.GetComponent<PlanetaryGravity>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<GravityControl>())
        {
            collision.GetComponent<GravityControl>().gravities.Remove(this.GetComponent<PlanetaryGravity>());

            if (collision.transform.parent == gameObject)
            {
                collision.transform.SetParent(collision.GetComponent<GravityControl>().getBiggestPlanetByMass());
            }
        }
    }

    public void OnDestroy()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Player")
            {
                transform.GetChild(i).GetComponent<GravityControl>().gravities.Remove(this);
                transform.GetChild(i).parent = null;
            }
        }
    }
}
