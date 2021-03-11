using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HierarchyHelpers : MonoBehaviour
{
    public static Transform GetFirstChildWithTag(Transform gm, string tag)
    {
        foreach(Transform child in gm)
        {
            // Debug.Log(child.tag);
            if (child.tag == tag)
            {
                return child;
            }
        }
        
        return null;
    }
}
