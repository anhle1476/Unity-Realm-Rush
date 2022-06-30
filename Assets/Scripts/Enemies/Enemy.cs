using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 100)]
        private int maxHealth = 5;

        private int _health;
        
        public int Heath
        {
            get => _health;
            protected set => _health = Mathf.Clamp(value, 0, maxHealth);
        }

        public bool IsDead => Heath <= 0;

        public virtual void ResetStats()
        {
            Heath = maxHealth;
        }

        public void Hit(int damage)
        {
            if (IsDead) return;
            
            Heath -= damage;
        }

        private void Start()
        {
            ResetStats();
        }

        #region override methods

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("Hit " + other);
            
            Hit(1);

            if (IsDead)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}