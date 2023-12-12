using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private List<GameObject> models;
    private GameObject selectedPrefab; // Store the selected prefab object
    private bool selectingMode = true; // Flag to indicate whether in selecting mode or character selection mode
    private int selectionIndex = 0; // Initialize selectionIndex

   //  public QuickInstantiatePUN quickInstantiateScript; 

   

    void Start()
    {
        models = new List<GameObject>();
        foreach (Transform t in transform)
        {
            models.Add(t.gameObject);
            t.gameObject.SetActive(false);
        }

        // Initialize with the default selection (index 0)
        Select(0);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && selectingMode)
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Mouse X") * -1, 0.0f));
    }

    public void Select(int index)
    {
        if (index == selectionIndex || !selectingMode)
            return;

        if (index < 0 || index >= models.Count)
            return;

        models[selectionIndex].SetActive(false);
        selectionIndex = index;
        models[selectionIndex].SetActive(true);

        selectedPrefab = models[selectionIndex]; // Update the selected prefab
    }

 public void SelectCharacter()
{
    selectingMode = false; // Stop selecting mode

    GameObject selectedPrefab = GetSelectedCharacterPrefab();
    if (selectedPrefab != null)
    {
       
        Debug.Log("Selected Prefab Set: " + selectedPrefab.name);
    }
    else
    {
        Debug.LogError("Invalid selected prefab reference!");
    }
}
    public GameObject GetSelectedCharacterPrefab()
    {
        return selectedPrefab; // Return the selected prefab object
    }
}