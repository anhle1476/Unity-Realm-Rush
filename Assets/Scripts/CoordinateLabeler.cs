using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// Use this instead of ExecuteInEditMode since it's being phased out (https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html)
[ExecuteAlways] 
public class CoordinateLabeler : MonoBehaviour
{
    private const int GRID_SIZE = 10;

    private TextMeshPro _label;

    private void Awake()
    {
        _label = GetComponent<TextMeshPro>();
        
        UpdatePropsByTileCoordinate();
    }

    private void Update()
    {
        if (Application.isPlaying) return;

        UpdatePropsByTileCoordinate();
    }

    private void UpdatePropsByTileCoordinate()
    {
        var tile = transform.parent;
        var tilePosition = tile.position;
        
        var coordinate = new Vector2(tilePosition.x, tilePosition.z) / GRID_SIZE;
        
        _label.text = coordinate.x + "," + coordinate.y;
        tile.name = coordinate.ToString("0");
    }
}
