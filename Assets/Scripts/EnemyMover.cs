using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private List<Waypoint> path = new List<Waypoint>();
    
    #region utils methods

    // /// <summary>
    // /// Get all available waypoints in scene and map to a dict.
    // /// </summary>
    // /// <returns></returns>
    // private IDictionary<string, Waypoint> PopulateWaypoints()
    // {
    //     var result = new Dictionary<string, Waypoint>();
    //
    //     var allWaypoints = FindObjectsOfType<Waypoint>();
    //     if (allWaypoints != null)
    //     {
    //         foreach (var waypoint in allWaypoints)
    //         {
    //             _waypointsDict[waypoint.name] = waypoint;
    //         }
    //     }
    //
    //     return result;
    // }

    #endregion

    #region movement methods

    /// <summary>
    /// Move enemy to a selected waypoint and return if the target is reached or not
    /// </summary>
    /// <param name="waypoint"></param>
    /// <returns>is target reached</returns>
    private bool MoveToWaypoint(Waypoint waypoint)
    {
        Vector3 currentPos = transform.position;
        Vector3 waypointPos = waypoint.transform.position;
        Vector3 targetPos = new Vector3(waypointPos.x, currentPos.y, waypointPos.z);

        transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(currentPos, targetPos, Time.deltaTime * speed);
        
        return transform.position == targetPos;
    }

    #endregion
    
    #region override methods
    
    private void FixedUpdate()
    {
        MoveThroughPath();
    }

    private void MoveThroughPath()
    {
        Waypoint targetWaypoint = path.FirstOrDefault();
        if (!targetWaypoint) return;
        
        var isReached = MoveToWaypoint(targetWaypoint);
        if (isReached)
        {
            path.RemoveAt(0); // make next one the new target waypoint
        }
    }

    #endregion
}
