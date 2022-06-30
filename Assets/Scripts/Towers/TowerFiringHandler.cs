using System;
using UnityEngine;

namespace Towers
{
    [RequireComponent(typeof(TargetLocator))]
    public class TowerFiringHandler : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem weapon;
        
        private TargetLocator _targetLocator;
        
        private void Start()
        {
            _targetLocator = GetComponent<TargetLocator>();
        }

        private void Update()
        {
            if (!weapon) 
                return;

            if (_targetLocator.HasTarget)
            {
                if (weapon.isPlaying == false)
                {
                    weapon.Play();
                }
            }
            else
            {
                if (weapon.isPlaying)
                {
                    weapon.Stop();
                }
            }
        }
    }
}