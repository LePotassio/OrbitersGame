using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarCamController : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            if (currentPos.z < -20)
            this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + (10000 * Time.fixedDeltaTime));
        }
        else if(Input.GetKey(KeyCode.X))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            if (currentPos.z > -60000)
                this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - (10000 * Time.fixedDeltaTime));
        }


        if (Input.GetKey(KeyCode.I))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            if (currentPos.y < 90000)
                this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y + (10000 * Time.fixedDeltaTime), currentPos.z);
        }

        if (Input.GetKey(KeyCode.K))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            if (currentPos.y > -90000)
                this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y - (10000 * Time.fixedDeltaTime), currentPos.z);
        }

        if (Input.GetKey(KeyCode.L))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            if (currentPos.x < 90000)
                this.gameObject.transform.position = new Vector3(currentPos.x + (10000 * Time.fixedDeltaTime), currentPos.y, currentPos.z);
        }

        if (Input.GetKey(KeyCode.J))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            if (currentPos.x > -90000)
                this.gameObject.transform.position = new Vector3(currentPos.x - (10000 * Time.fixedDeltaTime), currentPos.y, currentPos.z);
        }
    }
}
