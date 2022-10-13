using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddWaterStream : MonoBehaviour
{
    public GameObject enableWater;
    public GameObject itself;
    public GameObject floor, floor2;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enableWater.GetComponent<WaterStream>().enabled = true;
            floor.gameObject.SetActive(false);
            floor2.gameObject.SetActive(false);
            itself.gameObject.SetActive(false);
        }
    }
}
