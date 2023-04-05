using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public float respawnDelay = 2f;

    private void Start()
    {
        SpawnEnemy();
        SpawnCoin();
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    private void SpawnCoin()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10));
        Instantiate(coinPrefab, randomPosition, Quaternion.identity);
    }

    public void RespawnEnemy()
    {
        StartCoroutine(RespawnEnemyCoroutine());
    }

    public void RespawnCoin()
    {
        StartCoroutine(RespawnCoinCoroutine());
    }

    private IEnumerator RespawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }

    private IEnumerator RespawnCoinCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnCoin();
    }
}

