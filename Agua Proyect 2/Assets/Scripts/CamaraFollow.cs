using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform target;

    public float fsmoothSpeed = 0.125f;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPosition, fsmoothSpeed);
        transform.position = smoothPos;

        
    }
}
