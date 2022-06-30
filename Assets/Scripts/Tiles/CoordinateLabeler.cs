using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// Use this instead of ExecuteInEditMode since it's being phased out (https://docs.unity3d.com/ScriptReference/ExecuteInEditMode.html)
namespace Tiles
{
    [ExecuteAlways] 
    public class CoordinateLabeler : MonoBehaviour
    {
        private const int GRID_SIZE = 10;

        private TextMeshPro _label;
        private Waypoint _waypoint;

        private void Awake()
        {
            _waypoint = GetComponentInParent<Waypoint>();
            _label = GetComponent<TextMeshPro>();
            _label.enabled = false;
            
            UpdatePropsByTileCoordinate();
        }

        private void Update()
        {
            if (!Application.isPlaying)
            {
                UpdatePropsByTileCoordinate();
            }
            
            ColorCoordinate();
            ToggleLabels();
        }

        private void ToggleLabels()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _label.enabled = !_label.IsActive();
            }
        }

        private void UpdatePropsByTileCoordinate()
        {
            Transform tile = transform.parent;
            Vector3 tilePosition = tile.position;
        
            Vector2 coordinate = new Vector2(tilePosition.x, tilePosition.z) / GRID_SIZE;
        
            _label.text = coordinate.x + "," + coordinate.y;
            tile.name = coordinate.ToString("0");
        }

        private void ColorCoordinate()
        {
            if (_waypoint.IsPlaceable) return;
            
            Color baseColor = _label.color;
            _label.color = baseColor.WithAlpha(0.5f);
        }
    }
}
