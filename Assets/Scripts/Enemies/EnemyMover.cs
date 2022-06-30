using System.Collections;
using System.Collections.Generic;
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

        private readonly YieldInstruction _followPathWaiter = new WaitForEndOfFrame();
    
        private void Start()
        {
            StartCoroutine(FollowPath());
        }

        private IEnumerator FollowPath()
        {
            foreach (Waypoint waypoint in path)
            {
                Vector3 startPos = transform.position;
                Vector3 endPos = waypoint.transform.position;

                var travelPercent = 0f;
            
                transform.LookAt(endPos);
                while (travelPercent < 1f)
                {
                    travelPercent += Time.deltaTime * speed;
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    yield return _followPathWaiter;
                }
            
            }
        }
    }
}
