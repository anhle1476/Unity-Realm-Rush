using GameManagers;
using UnityEngine;

namespace Tiles
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] 
        private bool isPlaceable = true;

        [SerializeField]
        private bool isRunnable = false;
        
        private TowerSelector _towerSelector;

        #region getter
        
        public bool IsPlaceable => isPlaceable;
        public bool IsRunnable => isRunnable;

        #endregion

        #region waypoint name

        /// <summary>
        /// generate waypoint name by position
        /// </summary>
        /// <param name="waypointPos">waypoint 3D position</param>
        /// <returns></returns>
        public static string NameByPosition(Vector3 waypointPos)
        {
            return NameByPosition(waypointPos.x, waypointPos.z);
        }
        
        /// <summary>
        /// generate waypoint name by position
        /// </summary>
        /// <param name="x">X position in 2D</param>
        /// <param name="y">Y position in 2D, it's the tile Z position in 3D</param>
        /// <returns></returns>
        public static string NameByPosition(float x, float y)
        {
            return (new Vector2(x, y) / GlobalConstant.GRID_SIZE).ToString("0");
        }

        #endregion
        
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
            isRunnable = false;
        }
    }
}