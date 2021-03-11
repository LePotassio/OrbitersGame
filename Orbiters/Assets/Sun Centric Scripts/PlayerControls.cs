using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Time.timeScale != 64) {
                Time.timeScale *= 2;
            }
            Debug.Log("Unity Timescale now: " + Time.timeScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Time.timeScale != 1) {
                Time.timeScale /= 2;
            }
            Debug.Log("Unity Timescale now: " + Time.timeScale);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Time.timeScale = 1;
            Debug.Log("Unity Timescale now: " + Time.timeScale);
        }
    }
}
