using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{

    public GameObject[] ObjectsToSpawn;

    void SpawnOne(){
        if (ObjectsToSpawn.Length < 1){
            return;
        }

        int index = Random.Range(0,ObjectsToSpawn.Length);
        //Debug.Log("Rand: " + index);
        GameObject gobj = Instantiate(ObjectsToSpawn[index], new Vector3(Random.Range(-10, 10), 0, Random.Range(-3,10)), ObjectsToSpawn[index].transform.rotation);
        Coin coin = gobj.GetComponent<Coin>();
        if (coin != null){
            coin.SetValue(100);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("SpawnObjects length: " + ObjectsToSpawn.Length);
        InvokeRepeating("SpawnOne", 5f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
