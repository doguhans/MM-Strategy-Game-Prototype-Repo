using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public Canvas canvasToDeactivate; // Reference to the canvas to activate
    public GameObject GObjToActivate; // 1'st Reference to the canvas to deactivate
   // public Canvas canvasToDeactivate2; // 2'nd Reference to the canvas to deactivate
    // Method to switch between canvases
    public void SwitchCanvas()
    {
        canvasToDeactivate.gameObject.SetActive(false); // Activate the canvas to be shown
        GObjToActivate.gameObject.SetActive(true); // Deactivate the other canvases
    //    canvasToDeactivate2.gameObject.SetActive(false);// Deactivate the other canvases
    }
}