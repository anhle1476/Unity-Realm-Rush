using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tiles;
using UnityEngine;

/// <summary>
/// Handle finding the shortest path to the destination.
///
/// <para>
/// Terminology:
/// Waypoint - The game tiles,
/// Path - the waypoint that is path, enemy can move to these waypoints 
/// </para>
/// </summary>
public class PathFinder
{
    #region singleton instance

    private static PathFinder s_pathFinder;
        
    public static PathFinder Instance => s_pathFinder ??= new PathFinder();

    #endregion
        
    private IDictionary<string, Waypoint> _waypointMap;
    private Waypoint _target;
    /// <summary>
    /// ! max recursive depth to prevent stack overflow
    /// </summary>
    private int maxDepth = 1000;
        
    private readonly IDictionary<string, ICollection<Waypoint>> _cachedPath = new Dictionary<string, ICollection<Waypoint>>();

    /// <summary>
    /// To be called on Awake of the Path finding game object
    /// </summary>
    /// <param name="targetWaypoints"></param>
    /// <param name="waypoints"></param>
    public void Instantiate(Waypoint targetWaypoints, IEnumerable<Waypoint> waypoints)
    {
        _target = targetWaypoints;
        _waypointMap = waypoints.ToDictionary(waypoint => waypoint.name, waypoint => waypoint);
        _cachedPath.Clear();
    }

    /// <summary>
    /// Get all available surrounding waypoints that is runnable
    /// </summary>
    /// <param name="waypoint"></param>
    /// <returns></returns>
    private IEnumerable<Waypoint> GetSurroundingAvailablePaths(Waypoint waypoint)
    {
        var result = new List<Waypoint>();

        float baseX = waypoint.transform.position.x;
        float baseY = waypoint.transform.position.z;
        var surroundingNames = new List<string>
        {
            Waypoint.NameByPosition(baseX + GlobalConstant.GRID_SIZE, baseY),
            Waypoint.NameByPosition(baseX - GlobalConstant.GRID_SIZE, baseY),
            Waypoint.NameByPosition(baseX, baseY + GlobalConstant.GRID_SIZE),
            Waypoint.NameByPosition(baseX, baseY - GlobalConstant.GRID_SIZE),
        };

        foreach (string waypointName in surroundingNames)
        {
            if (_waypointMap.TryGetValue(waypointName, out Waypoint surroundingWaypoint)
                && surroundingWaypoint.IsRunnable)
            {
                result.Add(surroundingWaypoint);
            }
        }

        return result;
    }

    public IEnumerable<Waypoint> GetShortestPath(Waypoint start)
    {
        if (_cachedPath.TryGetValue(start.name, out var cachedPath) 
            && IsCachedPathValid(cachedPath))
        {
            return cachedPath;
        }

        bool isFound = BuildShortestPaths(start, out var result);
        if (isFound)
        {
            BuildShortestPathCache(result);
        }
        
        return result;
    }

    /// <summary>
    /// BFS the surrounding paths (waypoint that's enemy can run) to find the shortest path
    /// </summary>
    /// <param name="startWaypoint"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    private bool BuildShortestPaths(Waypoint startWaypoint, out LinkedList<Waypoint> result)
    {
        result = new LinkedList<Waypoint>();
        string targetName = _target.name;

        if (_waypointMap.ContainsKey(startWaypoint.name) == false || _waypointMap.ContainsKey(targetName) == false)
        {
            return false;
        }
        // Key is the waypoint name and Value is the previous waypoint
        var previousDict = new Dictionary<string, Waypoint>{ {startWaypoint.name, null} };
        var visitedWPs = new HashSet<string>();
        var queue = new Queue<Waypoint>();
        
        queue.Enqueue(startWaypoint);

        bool isFound = false;
        while (queue.Any())
        {
            Waypoint currentWP = queue.Dequeue();
            string currentWpName = currentWP.name;

            if (visitedWPs.Contains(currentWpName))
            {
                continue;
            }

            visitedWPs.Add(currentWpName);
            
            if (targetName == currentWpName)
            {
                isFound = true;
                break;
            }

            foreach (Waypoint surroundingPath in GetSurroundingAvailablePaths(currentWP))
            {
                if (!visitedWPs.Contains(surroundingPath.name))
                {
                    queue.Enqueue(surroundingPath);
                    previousDict[surroundingPath.name] = currentWP;
                }
            }
        }

        if (isFound)
        {
            Waypoint currentWP = _target;
            while (currentWP)
            {
                result.AddFirst(currentWP);
                previousDict.TryGetValue(currentWP.name, out Waypoint previousWP);
                currentWP = previousWP;
            }
        }

        return isFound;
    }

    /// <summary>
    /// Rebuild the cache for all waypoints belong to the result shortest path.
    /// <example>For path of [A, B, C], we will have the cached paths {A: [A, B, C], B: [B, C], C: [C]}</example>
    /// </summary>
    /// <param name="path"></param>
    private void BuildShortestPathCache(IEnumerable<Waypoint> path)
    {
        var builtCache = new HashSet<string>();
        foreach (Waypoint waypoint in path)
        {
            _cachedPath[waypoint.name] = new List<Waypoint>();

            builtCache.Add(waypoint.name);
            foreach (string cacheName in builtCache)
            {
                _cachedPath[cacheName].Add(waypoint);
            }
        }
    }

    /// <summary>
    /// Check if the cached path is valid (all child waypoints are runable)
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private bool IsCachedPathValid(IEnumerable<Waypoint> path)
    {
        return path.Any() && path.All(waypoint => waypoint && waypoint.IsRunnable);
    }
}