using System;
using System.Collections;
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
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private float spawnTimer = 2f;


        public IEnumerable<Enemy> Enemies => enemies;

        private PathManager _pathManager;

        private void Start()
        {
            // initialize the list with all available enemies on the scene
            enemies = FindObjectsOfType<Enemy>()?.ToList() ?? new List<Enemy>();
            _pathManager = FindObjectOfType<PathManager>();
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                Vector3 spawnPosition = _pathManager.StartWaypoint.transform.position;
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                enemies.Add(newEnemy.GetComponent<Enemy>());

                yield return new WaitForSeconds(spawnTimer);
            }
        }
        
    }
}