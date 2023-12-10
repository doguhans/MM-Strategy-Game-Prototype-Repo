using UnityEngine;

public class ResourceCollection : MonoBehaviour
{
    public int resourceCount = 0; // Total count of resources collected
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Resource")|| other.CompareTag("resource")) // Check if collided with an object tagged as "Resource"
        {
            CollectResource(other.gameObject);
        }
    }

    void CollectResource(GameObject resource)
    {
        // Destroy the collected resource
        Destroy(resource);
        

        // Increase the resource count
        resourceCount++;
        Debug.Log("Wood collected: "+ resourceCount);
        
    } 
}
