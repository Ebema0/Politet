using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour

    
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
       Player = GameObject.FindWithTag("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
    }
}
