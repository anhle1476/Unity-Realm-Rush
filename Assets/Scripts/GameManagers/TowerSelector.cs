using UnityEngine;

namespace GameManagers
{
    public class TowerSelector : MonoBehaviour
    {
        [SerializeField] 
        private GameObject selectedTowerPrefab;
    
        public GameObject SelectedTowerPrefab { get => selectedTowerPrefab; set => selectedTowerPrefab = value; }
    }
}
