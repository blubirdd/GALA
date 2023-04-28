using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToBeSpawned;
    [SerializeField] private int numberOfItems;
    [SerializeField] private float spawnDelay = 5f; // time in seconds between spawns
    public HealthBar pHealth;
    public float damage;

    public GameObject canvasParent;

    private List<GameObject> spawnedObjects = new List<GameObject>(); // list to store spawned objects

    [Header("Addition")]
    public GameObject hitPrefab;
    public GameObject canvas;
    public GameObject gameCanvas;
    [Header("Audio")]
    public AudioSource hitSFX;
    public AudioClip hitSFXClip;

    public Transform centerPoint;
    private void Start()
    {
        // Add a BoxCollider2D component to detect collisions
        gameObject.AddComponent<BoxCollider2D>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void StartGame()
    {
        StopAllCoroutines();
        // Start spawning objects
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        float initialDelay = spawnDelay; // store the initial spawn delay

        while (true)
        {
            // Calculate the new spawn delay based on the elapsed time
            float elapsedSeconds = Time.time;
            float newDelay = initialDelay - (elapsedSeconds / 100f); // change the 10f to adjust the difficulty curve

            for (int i = 0; i < numberOfItems; i++)
            {
                Vector3 position = new Vector3(Random.Range(-5f, 10f), Random.Range(-5f, 10f), Random.Range(-5f, 10f));
                GameObject newObject = GameObject.Instantiate(objectToBeSpawned, position, Quaternion.identity);
                //newObject.transform.SetParent(canvasParent.transform, false);

                newObject.transform.localScale = new Vector3(3, 3, 3); // set new scale values

                // Add a new component to the spawned object to handle mouse clicks
                newObject.AddComponent<ClickableObject>();

                spawnedObjects.Add(newObject); // add new object to list
            }

            // Wait for the specified delay before spawning more objects
            yield return new WaitForSeconds(newDelay);
        }
    }

    // Method to destroy a spawned object and remove it from the list
    public void DestroyObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);

        hitSFX.PlayOneShot(hitSFXClip);

        // Get the sprite renderer component and store its color
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        Color startColor = spriteRenderer.color;

        // Determine the direction to move the object (left or right)
        Vector3 targetPos = obj.transform.position;
        targetPos.x += targetPos.x < 0 ? -10f : 10f;

        // Use DOTween to move the object to the target position and fade it out over 1 second
        obj.transform.DOMove(targetPos, 1f);
        spriteRenderer.DOColor(new Color(startColor.r, startColor.g, startColor.b, 0f), 0.3f).OnComplete(() => {
            // Destroy the object after the fade animation completes
            Destroy(obj);
        });
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
        EnemySpawner EnemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        EnemySpawner.DestroyObject(gameObject);

    }
}