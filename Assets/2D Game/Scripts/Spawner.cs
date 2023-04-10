using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] private int numberOfItems;
    [SerializeField] private float spawnDelay = 5f; // time in seconds between spawns
    public HealthBar pHealth;
    public float damage;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // list to store spawned objects

    private void Start()
    {
        // Add a BoxCollider2D component to detect collisions
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
        
        // Start spawning objects
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                Vector3 position = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
                GameObject newObject = GameObject.Instantiate(objectToBeSpawned, position, Quaternion.identity);
                newObject.transform.localScale = new Vector3(1, 1, 1); // set new scale values

                // Add a new component to the spawned object to handle mouse clicks
                newObject.AddComponent<ClickableObject>();

                spawnedObjects.Add(newObject); // add new object to list
            }

            // Wait for the specified delay before spawning more objects
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    // Method to destroy a spawned object and remove it from the list
    public void DestroyObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
        Destroy(obj);
    }

    // Use OnTriggerEnter2D instead of OnCollisionEnter2D
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Egg"))
        {
            pHealth.health -= damage;
        }
    }
}

// Class to handle mouse clicks on spawned objects
public class ClickableObject : MonoBehaviour
{
    private void OnMouseDown()
    {
        // Get the Spawner component and call the DestroyObject method with this gameobject
        Spawner spawner = GameObject.FindObjectOfType<Spawner>();
        spawner.DestroyObject(gameObject);
    }
}
