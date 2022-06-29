using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Use this instead of ExecuteInEditMode since it's being phased out (https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html)
[ExecuteAlways] 
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    private int gridSize = 10;
    
    private TextMeshPro _label;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        UpdateCoordinate();
    }

    private void Update()
    {
        if (!Application.isPlaying) return;
        
        UpdateCoordinate();
    }

    private void UpdateCoordinate()
    {
        var position = transform.parent.position;
        var xPos = position.x / gridSize;
        var yPos = position.z / gridSize;

        _label.text = xPos + "," + yPos;
    }
}
