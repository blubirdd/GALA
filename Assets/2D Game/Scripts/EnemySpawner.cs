using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject kingcobra;

    [SerializeField]
    private float kingcobraInterval = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(kingcobraInterval, kingcobra));
    }

    // Update is called once per frame
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5, 5), Random.Range(-6f, 6f),0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
