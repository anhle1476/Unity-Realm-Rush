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
        [SerializeField]
        private bool spawnEnemy = true;
        [SerializeField]
        private int initialPoolSize = 20;
        
        public IEnumerable<Enemy> Enemies => enemies;

        private PathManager _pathManager;

        private void Start()
        {
            // initialize the list with all available enemies on the scene
            enemies = FindObjectsOfType<Enemy>()?.ToList() ?? new List<Enemy>();
            _pathManager = FindObjectOfType<PathManager>();

            PopulateEnemyPool();
            
            StartCoroutine(SpawnEnemy());
        }

        private void PopulateEnemyPool()
        {
            while (enemies.Count() < initialPoolSize)
            {
                InstantiateNewEnemyInPool();
            }
        }

        private GameObject InstantiateNewEnemyInPool()
        {
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, transform);
            newEnemy.SetActive(false);
            
            enemies.Add(newEnemy.GetComponent<Enemy>());
            return newEnemy;
        }

        private void ActivateEnemyInPool()
        {
            Enemy inactivateEnemy = enemies.FirstOrDefault(enemy => enemy.gameObject.activeInHierarchy == false);
            if (inactivateEnemy)
            {
                inactivateEnemy.gameObject.SetActive(true);
            }
            else
            {
                InstantiateNewEnemyInPool().SetActive(true);
            }
        }

        private IEnumerator SpawnEnemy()
        {
            while (true)
            {
                if (spawnEnemy)
                {
                    ActivateEnemyInPool();
                }

                yield return new WaitForSeconds(spawnTimer);
            }
        }
        
    }
}