using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance { get; private set; }

    private GameObject selectedPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedPrefab(GameObject prefab)
    {
        selectedPrefab = prefab;
        Debug.Log("Selected Prefab Set in PrefabManager: " + selectedPrefab.name); // Log to indicate selected prefab in PrefabManager
    }

    public GameObject GetSelectedPrefab()
    {
        return selectedPrefab;
    }
}
