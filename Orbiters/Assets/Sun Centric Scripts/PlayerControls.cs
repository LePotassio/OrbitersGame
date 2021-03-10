using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log(Time.timeScale);
            if (Time.timeScale != 64) {
                Time.timeScale *= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.timeScale != 1) {
                Time.timeScale /= 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 1;
        }
    }
}
