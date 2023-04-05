using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpawnCoinScript : MonoBehaviour
{
    public GameObject theCoin;
    public float respawnDelay = 2f;
    public int coinCount = 10;

    public static event Action<SpawnCoinScript> OnCoinCollected;

    private void OnDisable()
    {
        OnCoinCollected?.Invoke(this);
    }

    void Update()
    {
        gameObject.transform.Rotate(0f, 1f, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(-10f, 10f), 1f, UnityEngine.Random.Range(-10f, 10f));
            Instantiate(theCoin, randomPosition, Quaternion.identity);
            Destroy(gameObject);
            GameObject coin = other.gameObject;
            coin.SetActive(false);
            RespawnPlayer(coin);
            OnCoinCollected?.Invoke(this);
        }
    }

    private void RespawnPlayer(GameObject coin)
    {
        StartCoroutine(RespawnCoroutine(coin));
    }

    private IEnumerator RespawnCoroutine(GameObject coin)
    {
        coin.SetActive(true);
        yield return new WaitForSeconds(respawnDelay);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(0f, 1f, 0f);
    }
}
