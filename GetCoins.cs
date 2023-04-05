using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoins : MonoBehaviour
{

    public AudioSource CollectCoinSound;
    private void OnCollisionEnter(Collision coin)

    {
        if (coin.collider.name == "Player")
        {
            Destroy(this.gameObject);
            CollectCoinSound.Play();
        }

    }
}
