using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    //public MyEventSystem eventSystem;

    // A static Action for the Enemy-class used
    // to register "listeners" / "subsribers".
    // Because it's static, this registry is common
    // for all Enemy-class objects!
    public static event Action<Enemy> OnEnemyDie;


    // The value of this enemy used to increase score
    public int enemyValue { get; private set; }


    private void OnDisable()
    {
        enemyValue = UnityEngine.Random.Range(1, 5);

        // Invoke the event for each registered "listener"
        // if the OnEnemyDie has some registered (the ?-op)
        OnEnemyDie?.Invoke(this);
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
