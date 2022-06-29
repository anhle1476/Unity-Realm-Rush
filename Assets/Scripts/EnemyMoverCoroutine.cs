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

    private readonly WaitForSeconds waiter = new WaitForSeconds(1);
    
    private void Start()
    {
        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in path)
        {
            Vector3 waypointPos = waypoint.transform.position;
            transform.position = new Vector3(waypointPos.x, transform.position.y, waypointPos.z);
            yield return waiter;
        }
    }
}
