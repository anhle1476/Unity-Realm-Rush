using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameManagers;
using Tiles;
using UnityEngine;

namespace Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField]
        private List<Waypoint> path = new List<Waypoint>();
        [SerializeField]
        [Range(0f, 5f)]
        private float speed = 1f;

        private static readonly YieldInstruction _followPathYieldInstruction = new WaitForEndOfFrame();
        private Coroutine _followPathCoroutine;

        private Waypoint _currentWaypoint;
        
        private void Start()
        {
            _currentWaypoint = FindObjectOfType<PathManager>()?.StartWaypoint;
            StartFollowNewPath();
        }

        private void StartFollowNewPath()
        {
            if (_followPathCoroutine != null)
            {
                StopCoroutine(_followPathCoroutine);
            }
            
            FindNewPath();
            _followPathCoroutine = StartCoroutine(FollowPath());
        }

        private void FindNewPath()
        {
            // ToList to make a copy so it will not affect the one in cache
            path = PathFinder.Instance.GetShortestPath(_currentWaypoint).ToList();
        }

        private IEnumerator FollowPath()
        {
            while (path.Any())
            {
                Waypoint waypoint = path.First();
                
                Vector3 startPos = transform.position;
                Vector3 endPos = waypoint.transform.position;

                transform.LookAt(endPos);
                
                var isTravelOriginalPath = true;
                var travelPercent = 0f;
                while (travelPercent < 1f)
                {
                    // find new path if the current one is no longer usable
                    if (waypoint.IsRunnable == false)
                    {
                        FindNewPath();
                        isTravelOriginalPath = false;
                        break;
                    }
                    
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    yield return _followPathYieldInstruction;
                }

                if (isTravelOriginalPath)
                {
                    _currentWaypoint = waypoint;
                    path.RemoveAt(0);
                    
                }
            }
            
            Destroy(gameObject);
        }
    }
}
