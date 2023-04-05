using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public static event Action<Coin> OnCoinCollected;

    public int coinValue {get; private set; }

    public void SetValue(int value){
        this.coinValue = value;
    }

    private void OnDisable(){
        // Invoke the methods that have been registered
        OnCoinCollected?.Invoke(this);
    } 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
