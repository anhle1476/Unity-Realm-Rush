using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace GameManagers
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField]
        private List<Enemy> enemies;

        public IEnumerable<Enemy> Enemies => enemies;
        
        private void Start()
        {
            // initialize the list with all available enemies on the scene
            enemies = FindObjectsOfType<Enemy>()?.ToList() ?? new List<Enemy>();
        }
        
    }
}