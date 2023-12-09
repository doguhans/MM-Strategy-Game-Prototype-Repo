using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public LayerMask clickMask; // Define the layer mask to detect clicks/taps
    public float clickRaycastDistance = 100f; // Max distance to cast a ray for click detection

    AStar pathfinder;
    bool isMoving = false;
    Vector3 targetPosition;

    void Start()
    {
        pathfinder = GetComponent<AStar>();
        Vector3 startPos = transform.position; // Set your starting position here (e.g., player's position)
        targetPosition = startPos; // Set initial target position to player's position
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isMoving)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, clickRaycastDistance, clickMask))
            {
                Node clickedNode = hit.collider.GetComponent<Node>(); // Assuming each node has its collider
                if (clickedNode != null && clickedNode.walkable)
                {
                    // Set the clicked node as the new target position
                    targetPosition = clickedNode.worldPosition;
                    FindPathAndMove();
                }
            }
        }
    }

    void FindPathAndMove()
    {
        Vector3 startPos = transform.position;
        List<Node> path = pathfinder.FindPath(startPos, targetPosition);

        if (path != null)
        {
            // Path found
            isMoving = true;
            StartCoroutine(MoveAlongPath(path));
        }
        else
        {
            // No path found
            Debug.Log("No path found!");
        }
    }

    IEnumerator MoveAlongPath(List<Node> path)
    {
        foreach (Node node in path)
        {
            // Move the player to the node's position (node.worldPosition)
            // Example: transform.position = node.worldPosition;

            // For demonstration, a simple movement towards the node position is shown
            float duration = 0.5f; // Adjust the movement speed here
            float elapsedTime = 0f;
            Vector3 startingPos = transform.position;
            Vector3 targetPos = node.worldPosition;

            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(startingPos, targetPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPos;

            yield return new WaitForSeconds(0.1f); // Add a small delay between movements
        }

        isMoving = false;
    }
}
