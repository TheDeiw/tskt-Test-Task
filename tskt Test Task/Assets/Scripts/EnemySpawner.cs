using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;

    public float minSpawnRadius = 10f;
    public float maxSpawnRadius = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minSpawnRadius, maxSpawnRadius);

        Vector3 offset = new Vector3(Mathf.Cos(angle) * distance, 0f, Mathf.Sin(angle) * distance);
        Vector3 spawnPosition = player.transform.position + offset;

        if (!Physics.CheckSphere(spawnPosition, 1f))
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
        //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
