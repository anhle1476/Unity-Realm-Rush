using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    private int gridSize = 10;
    
    private TextMeshPro _label;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        var position = transform.parent.position;
        var xPos = position.x / gridSize;
        var yPos = position.z / gridSize;

        _label.text = xPos + "," + yPos;
    }
}
