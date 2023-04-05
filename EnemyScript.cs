using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject explosionEffect;
    public GameObject enemyPrefab;
    public float health = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 5f;
            Destroy(collision.gameObject);

            if (health <= 0f)
            {
                Destroy(gameObject);
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

                // Instantiate a new enemy
                Instantiate(enemyPrefab, new Vector3(Random.Range(-10, 10), 0.5f, Random.Range(-10, 10)), Quaternion.identity);
            }
        }
    }
}
