 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public GameObject enemyPrefab; 
    public Vector3 enemySpawnPosition; 
    private GameObject enemyInstance; 
    int wholeNumber = 3;
    float decimalNumber = 3; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SpawnEnemy();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(0, 5, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector3(0, 0, 5);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector3(5, 0, 0);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector3(0, 0, -5);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector3(-5, 0, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            RespawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        enemyInstance = Instantiate(enemyPrefab, enemySpawnPosition, Quaternion.identity);
    }

    void RespawnEnemy()
    {
        Destroy(enemyInstance);
        SpawnEnemy();
    }
}

