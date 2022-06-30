using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoverCoroutine : MonoBehaviour
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
            Vector3 waypointPos = waypoint.transform.position;
            Vector3 startPos = transform.position;
            Vector3 endPos = new Vector3(waypointPos.x, transform.position.y, waypointPos.z);

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
