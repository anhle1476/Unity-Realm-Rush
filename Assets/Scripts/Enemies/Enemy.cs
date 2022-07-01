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

        private int Heath
        {
            get => _health;
            set => _health = Mathf.Clamp(value, 0, maxHealth);
        }

        public bool IsDead => Heath <= 0;

        protected virtual void ResetStats()
        {
            Heath = maxHealth;
        }

        private void Hit(int damage)
        {
            if (IsDead) return;
            
            Heath -= damage;
        }

        private void OnEnable()
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
                gameObject.SetActive(false);
            }
        }

        #endregion
    }
}