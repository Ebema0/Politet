using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{

    public float RotationSpeed = 360.0f;

    private Quaternion startRotation;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = Quaternion.Euler(0, RotationSpeed*Time.time, 0);
        transform.rotation = rot*startRotation;
        
    }
}
