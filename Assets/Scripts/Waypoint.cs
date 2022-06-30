using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField]
    private bool isPlaceable = true; 
    
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Debug.Log("Mouse down " + name);
        }
    }
}
