using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerAStarMovement : MonoBehaviourPun
{
    public LayerMask walkableLayerMask; // Assign the layer mask for walkable areas in the Inspector
    private Grid grid;
    private List<Grid.Node> currentPath;
    private int currentPathIndex;

    private Vector3 previousPosition;
    [SerializeField] float movementSpeed;

    private Animator _animator;

    private void Start()
    {    // Initialize the previous position with the object's starting position
        previousPosition = transform.position;
        _animator = GetComponent<Animator>();
        grid = FindObjectOfType<Grid>(); // Assuming the Grid script is attached to a GameObject in the scene
        currentPath = new List<Grid.Node>();
        currentPathIndex = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && base.photonView.IsMine) // Assuming left-click or tap for movement
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayerMask))
            {
                if (hit.collider != null)
                {
                    Vector3 targetPosition = hit.point;
                    MoveToPosition(targetPosition);
                }
            }
           
        }

        if (currentPath.Count > 0 && currentPathIndex < currentPath.Count)
        {
            MoveAlongPath();
        }

        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Check if the object is moving along X and Z axes
        bool isMovingX = currentPosition.x != previousPosition.x;
        bool isMovingZ = currentPosition.z != previousPosition.z;

        if (isMovingX || isMovingZ)
        {
             UpdateMovingBoolean(true);
        }
        else
        {
             UpdateMovingBoolean(false);
        }

        // Update the previous position to the current position for the next frame
        previousPosition = currentPosition;      
    }

        private void UpdateMovingBoolean(bool moving)
    {

        _animator.SetBool("Moving", moving );

    }

    private void MoveToPosition(Vector3 targetPosition)
    {
        currentPath = grid.FindPath(transform.position, targetPosition);

        if (currentPath != null && currentPath.Count > 0)
        {
            currentPathIndex = 0;
        }
        else
        {
            Debug.Log("Path not found or empty.");
        }
    }

    private void MoveAlongPath()
    {
        Vector3 targetWorldPos = currentPath[currentPathIndex].worldPosition;

        if (Vector3.Distance(transform.position, targetWorldPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, Time.deltaTime * movementSpeed);
        }
        else
        {
            currentPathIndex++;
        }
    }
}
