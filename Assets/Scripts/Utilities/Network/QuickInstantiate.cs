using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class QuickInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
     public float offsetXDistance = 3.0f; // Distance between instantiated objects in X axis

    private void Awake()
    {
        float totalOffsetX = 0f;

        for (int i = 0; i < 3; i++)
        {
            Vector3 position = new Vector3(transform.position.x + totalOffsetX,
                                           transform.position.y,
                                           transform.position.z);

            MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);

            totalOffsetX += offsetXDistance;
        }
    }
    private bool CheckPosition(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, offsetXDistance);

        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to the same type of prefab or a different one that you want to avoid overlapping with.
            if (collider.CompareTag("YourTag")) // Replace "YourTag" with the relevant tag of the objects you want to avoid overlapping with.
            {
                return false; // There is an object too close, don't instantiate here
            }
        }

        return true; // No nearby objects found, safe to instantiate
    }
}
