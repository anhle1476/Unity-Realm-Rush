using System;
using System.Collections;
using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace GameManagers
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField]
        private Waypoint startWaypoint;
        
        [SerializeField]
        private Waypoint targetWaypoint;
        
        private IEnumerable<Waypoint> _allWaypoints;

        public Waypoint StartWaypoint => startWaypoint;

        public Waypoint TargetWaypoint => targetWaypoint;

        private void Awake()
        {
            _allWaypoints = FindObjectsOfType<Waypoint>();
            PathFinder.Instance.Instantiate(targetWaypoint, _allWaypoints);
        }
    }
}