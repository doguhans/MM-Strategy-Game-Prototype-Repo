using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class QuickInstantiate : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject _prefab2;
    public float offsetXDistance = 3.0f; // Distance between instantiated objects in X axis

    private void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                SpawnPrefab(_prefab);
            }
            else
            {
                SpawnPrefab(_prefab2);
            }
        }
    }

    private void SpawnPrefab(GameObject prefabToInstantiate)
    {
        int maxAttempts = 100; // Maximum attempts to find a valid position
        float minDistanceSquared = offsetXDistance * offsetXDistance; // Minimum distance squared

        for (int i = 0; i < 1; i++)
        {
            bool positionFound = false;
            int attempts = 0;

            while (!positionFound && attempts < maxAttempts)
            {
                Vector3 position = new Vector3(transform.position.x + Random.Range(-offsetXDistance, offsetXDistance),
                                               transform.position.y,
                                               transform.position.z);

                if (CheckPosition(position, minDistanceSquared)) // Check if the position is valid
                {
                    MasterManager.NetworkInstantiate(prefabToInstantiate, position, Quaternion.identity);
                    positionFound = true;
                }

                attempts++;
            }
        }
    }

    private bool CheckPosition(Vector3 position, float minDistanceSquared)
    {
        Collider[] colliders = Physics.OverlapSphere(position, offsetXDistance);

        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to the object you want to avoid overlapping with.
            if (collider.CompareTag("Player")) // Replace "Player" with the relevant tag of the objects you want to avoid overlapping with.
            {
                return false; // There is an object too close, don't instantiate here
            }

            // Check distance between instantiated objects
            float sqrDistance = (position - collider.transform.position).sqrMagnitude;
            if (sqrDistance < minDistanceSquared)
            {
                return false; // Objects are too close, don't instantiate here
            }
        }

        return true; // No nearby objects found, safe to instantiate
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class QuickInstantiate : MonoBehaviour
// {
//     [SerializeField] private GameObject _prefab;
//     [SerializeField] private GameObject _prefab2;
//     public float offsetXDistance = 3.0f; // Distance between instantiated objects in X axis

//     private void Awake()
//     {
//         int maxAttempts = 100; // Maximum attempts to find a valid position
//         float minDistanceSquared = offsetXDistance * offsetXDistance; // Minimum distance squared

//         for (int i = 0; i < 1; i++)
//         {
//             bool positionFound = false;
//             int attempts = 0;

//             while (!positionFound && attempts < maxAttempts)
//             {
//                 Vector3 position = new Vector3(transform.position.x + Random.Range(-offsetXDistance, offsetXDistance),
//                                                transform.position.y,
//                                                transform.position.z);

//                 if (CheckPosition(position, minDistanceSquared)) // Check if the position is valid
//                 {
//                     MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);
//                     positionFound = true;
//                 }

//                 attempts++;
//             }
//         }
//     }

//     private bool CheckPosition(Vector3 position, float minDistanceSquared)
//     {
//         Collider[] colliders = Physics.OverlapSphere(position, offsetXDistance);

//         foreach (Collider collider in colliders)
//         {
//             // Check if the collider belongs to the object you want to avoid overlapping with.
//             if (collider.CompareTag("Player")) // Replace "Player" with the relevant tag of the objects you want to avoid overlapping with.
//             {
//                 return false; // There is an object too close, don't instantiate here
//             }

//             // Check distance between instantiated objects
//             float sqrDistance = (position - collider.transform.position).sqrMagnitude;
//             if (sqrDistance < minDistanceSquared)
//             {
//                 return false; // Objects are too close, don't instantiate here
//             }
//         }

//         return true; // No nearby objects found, safe to instantiate
//     }
// }






