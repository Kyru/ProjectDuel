using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject alert;
    private GameObject tempAlert;
    private List<GameObject> spawnPositions;
    private bool spawnNext;
    public string exitTowards; // right or left
    public int minSeconds;
    public int maxSeconds;
    void Start()
    {
        minSeconds = 5;
        maxSeconds = 10;
        spawnNext = true;
        spawnPositions = new List<GameObject>();
        foreach (Transform child in this.gameObject.transform)
        {
            spawnPositions.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNext) StartCoroutine("SpawnEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        if (exitTowards == "left")
        {
            spawnNext = false;
            int randomSeconds = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(randomSeconds);

            int randomPosition = Random.Range(0, 5);

            Instantiate(enemy, spawnPositions[randomPosition].transform.position, enemy.transform.rotation, gameObject.transform);
            tempAlert = Instantiate(alert, spawnPositions[randomPosition].transform.position + new Vector3(10f, 1f, 0),
                alert.transform.rotation, gameObject.transform);
            StartCoroutine("DestroyTempAlert");

            spawnNext = true;
        }
        else if (exitTowards == "right")
        {
            spawnNext = false;
            int randomSeconds = Random.Range(minSeconds, maxSeconds);
            yield return new WaitForSeconds(randomSeconds);

            int randomPosition = Random.Range(0, 5);

            Instantiate(enemy, spawnPositions[randomPosition].transform.position, Quaternion.Euler(0, -90, 0), gameObject.transform);
            tempAlert = Instantiate(alert, spawnPositions[randomPosition].transform.position + new Vector3(-10f, 1f, 0),
                alert.transform.rotation, gameObject.transform);
            StartCoroutine("DestroyTempAlert");

            spawnNext = true;
        }
    }

    IEnumerator DestroyTempAlert()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(tempAlert);
    }
}
