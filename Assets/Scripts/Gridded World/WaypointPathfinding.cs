using System.Collections.Generic;
using UnityEngine;

public class WaypointPathfinding : MonoBehaviour
{
    public Transform target;
    public List<Transform> waypoints; // Populate this list with your waypoints
    public LayerMask obstacleMask; // Set this in the Inspector to the layer containing obstacles

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToTarget(target.position);
        }
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        Transform nearestWaypoint = FindNearestWaypoint(transform.position);
        Transform targetWaypoint = FindNearestWaypoint(targetPosition);

        if (nearestWaypoint != null && targetWaypoint != null)
        {
            List<Transform> path = FindPath(nearestWaypoint, targetWaypoint);

            if (path != null && path.Count > 0)
            {
                StartCoroutine(MoveAlongPath(path));
            }
        }
    }

    private Transform FindNearestWaypoint(Vector3 position)
    {
        Transform nearest = null;
        float minDistance = float.MaxValue;

        foreach (Transform waypoint in waypoints)
        {
            float distance = Vector3.Distance(position, waypoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = waypoint;
            }
        }

        return nearest;
    }

    private List<Transform> FindPath(Transform start, Transform end)
    {
        List<Transform> path = new List<Transform>();
        List<Transform> openSet = new List<Transform>();
        HashSet<Transform> closedSet = new HashSet<Transform>();
        Dictionary<Transform, Transform> cameFrom = new Dictionary<Transform, Transform>();
        Dictionary<Transform, float> gScore = new Dictionary<Transform, float>();

        foreach (Transform waypoint in waypoints)
        {
            gScore[waypoint] = Mathf.Infinity;
        }

        gScore[start] = 0;
        openSet.Add(start);

        while (openSet.Count > 0)
        {
            Transform current = GetLowestFScore(openSet, gScore);

            if (current == end)
            {
                path = ReconstructPath(cameFrom, current);
                break;
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (Transform neighbor in GetNeighbors(current))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float tentativeGScore = gScore[current] + Vector3.Distance(current.position, neighbor.position);

                if (!openSet.Contains(neighbor))
                    openSet.Add(neighbor);
                else if (tentativeGScore >= gScore[neighbor])
                    continue;

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
            }
        }

        return path;
    }

    private Transform GetLowestFScore(List<Transform> openSet, Dictionary<Transform, float> gScore)
    {
        Transform lowest = null;
        float minFScore = Mathf.Infinity;

        foreach (Transform waypoint in openSet)
        {
            if (gScore[waypoint] < minFScore)
            {
                minFScore = gScore[waypoint];
                lowest = waypoint;
            }
        }

        return lowest;
    }

    private List<Transform> ReconstructPath(Dictionary<Transform, Transform> cameFrom, Transform current)
    {
        List<Transform> totalPath = new List<Transform> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            totalPath.Insert(0, current);
        }
        return totalPath;
    }

    private IEnumerable<Transform> GetNeighbors(Transform waypoint)
    {
        List<Transform> neighbors = new List<Transform>();

        foreach (Transform otherWaypoint in waypoints)
        {
            if (otherWaypoint != waypoint)
            {
                float distance = Vector3.Distance(waypoint.position, otherWaypoint.position);
                if (distance < 3f && !IsObstacleBetween(waypoint.position, otherWaypoint.position))
                {
                    neighbors.Add(otherWaypoint);
                }
            }
        }

        return neighbors;
    }

    private bool IsObstacleBetween(Vector3 start, Vector3 end)
    {
        RaycastHit[] hits = Physics.RaycastAll(start, end - start, Vector3.Distance(start, end), obstacleMask);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                return true;
            }
        }

        return false;
    }

    private System.Collections.IEnumerator MoveAlongPath(List<Transform> path)
    {
        foreach (Transform waypoint in path)
        {
            while (Vector3.Distance(transform.position, waypoint.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoint.position, Time.deltaTime * 2f);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
