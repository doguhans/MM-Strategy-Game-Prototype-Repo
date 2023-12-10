using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public PlayerAStarMovement movementScript; // Reference to the movement script for this object
    public Animator animator; // Reference to the Animator component for this object

    private static SelectableObject[] allSelectableObjects;
    private bool isSelected = false;

    private void Start()
    {
        // Disable movement and animator for all objects during initialization
        DisableAllMovementAndAnimator();
    }

    private void DisableAllMovementAndAnimator()
    {
        if (allSelectableObjects == null)
        {
            allSelectableObjects = FindObjectsOfType<SelectableObject>();
        }

        foreach (SelectableObject selectable in allSelectableObjects)
        {
            if (selectable.movementScript != null)
            {
                selectable.movementScript.enabled = false;
            }

            if (selectable.animator != null)
            {
                selectable.animator.enabled = false;
            }
        }
    }

    private void OnMouseDown()
    {
        // If the object is already selected, deselect it
        if (isSelected)
        {
            DeselectObject();
        }
        else
        {
            // Disable movement and animator for all objects
            DisableAllMovementAndAnimator();

            // Enable movement and animator for this object
            EnableObject();
        }
    }

    public void DeselectObject()
    {
        // Disable movement and animator for this object
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }

        if (animator != null)
        {
            animator.enabled = false;
        }

        isSelected = false;
    }

    public void EnableObject()
    {
        // Enable movement and animator for this object
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }

        if (animator != null)
        {
            animator.enabled = true;
        }

        isSelected = true;
    }
}
