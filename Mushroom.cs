using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public static event Action<Mushroom> OnMushroomCollected;

    [SerializeField]
    private float speedBoost = 5f;
    [SerializeField]
    private float boostDuration = 30f;

    private bool isBoostActive = false;
    private float boostEndTime;

    public int mushroomValue { get; private set; }

    public void SetValue(int value)
    {
        this.mushroomValue = value;
    }

    private void OnDisable()
    {
        
        OnMushroomCollected?.Invoke(this);

      
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
            
                playerController.IncreaseSpeed(speedBoost);
                isBoostActive = true;
                boostEndTime = Time.time + boostDuration;

                
                playerController.transform.localScale *= 1.5f; // increases the scale 
            }
        }
    }

    private void Update()
    {
        if (isBoostActive && Time.time >= boostEndTime)
        {
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    
                    playerController.ResetSpeed();
                    playerController.transform.localScale /= 1.5f; // decreases the scale 
                }
            }
            isBoostActive = false;
        }
    }
}
