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


        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 currentPos = this.gameObject.transform.position;
            
            this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y + (10000 * Time.fixedDeltaTime), currentPos.z);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 currentPos = this.gameObject.transform.position;

            this.gameObject.transform.position = new Vector3(currentPos.x, currentPos.y - (10000 * Time.fixedDeltaTime), currentPos.z);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 currentPos = this.gameObject.transform.position;

            this.gameObject.transform.position = new Vector3(currentPos.x + (10000 * Time.fixedDeltaTime), currentPos.y, currentPos.z);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 currentPos = this.gameObject.transform.position;

            this.gameObject.transform.position = new Vector3(currentPos.x - (10000 * Time.fixedDeltaTime), currentPos.y, currentPos.z);
        }
    }
}
