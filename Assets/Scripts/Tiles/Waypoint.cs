using GameManagers;
using UnityEngine;

namespace Tiles
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] 
        private bool isPlaceable = true;
        
        private TowerSelector _towerSelector;

        public bool IsPlaceable => isPlaceable;
        
        private void Start()
        {
            _towerSelector = FindObjectOfType<TowerSelector>();
        }

        private void OnMouseDown()
        {
            if (!isPlaceable) return;
            
            GameObject towerPrefab = _towerSelector.SelectedTowerPrefab;
            GameObject tower = Instantiate(towerPrefab, transform.position, Quaternion.identity);
            tower.name = $"{towerPrefab.name} {gameObject.name}";

            isPlaceable = false;
        }
    }
}