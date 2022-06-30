using System.Collections.Generic;
using Enemies;
using GameManagers;
using Unity.VisualScripting;
using UnityEngine;

namespace Towers
{
    /// <summary>
    /// Handle target register and aiming
    /// </summary>
    public class TargetLocator : MonoBehaviour
    {
        [SerializeField]
        [Range(0f, 200f)]
        private float range = 30f;
        [SerializeField]
        private List<GameObject> rotationParts = new List<GameObject>();
        
        private EnemyPool _enemyPool;
        private Enemy _targetEnemy;

        #region getter setter

        public virtual Enemy Target => _targetEnemy;

        public virtual bool HasTarget => _targetEnemy && !_targetEnemy.IsDead;

        #endregion
        
        #region private methods

        private bool IsTargetInRange(Component target, out float distance)
        {
            distance = Vector3.Distance(transform.position, target.transform.position);
            return distance <= range;
        }
        
        /// <summary>
        /// Register the closest enemy in range as the new target
        /// </summary>
        private void FindNewTarget()
        {
            Enemy closestEnemy = null;
            var closestDistance = float.MaxValue;

            foreach (Enemy enemy in _enemyPool.Enemies)
            {
                if (!enemy || enemy.IsDead 
                           || IsTargetInRange(enemy, out float distance) == false 
                           || distance >= closestDistance) 
                    continue;
                
                closestDistance = distance;
                closestEnemy = enemy;
            }

            _targetEnemy = closestEnemy;
        }
        
        /// <summary>
        /// Rotate the rotation parts aiming the target direction
        /// </summary>
        private void TargetAiming()
        {
            Vector3 targetPos = Target.transform.position;
            foreach (GameObject rotationPart in rotationParts)
            {
                // aiming at a position of the same height as the part. Otherwise the bullet might crashed to the ground
                var aimingPos = new Vector3(targetPos.x, rotationPart.transform.position.y, targetPos.z);
                rotationPart.transform.LookAt(aimingPos);
            }
        }

        #endregion

        #region override methods

        // Start is called before the first frame update
        private void Start()
        {
            _enemyPool = FindObjectOfType<EnemyPool>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (HasTarget == false || IsTargetInRange(Target, out _) == false)
            {
                FindNewTarget();
            }
            
            if (HasTarget == false) return;

            TargetAiming();
        }

        #endregion
    }
}
