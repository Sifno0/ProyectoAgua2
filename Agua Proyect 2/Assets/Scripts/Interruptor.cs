using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interruptor : MonoBehaviour
{
    public bool bIsActivated = false;

    public float fFireInContainer = 100;

    public GameObject GOa, GOb;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void GiveWater(float water)
    {
        fFireInContainer -= water;

        if (fFireInContainer == 0)
        {
            GOa.SetActive(false);
            GOb.SetActive(false);
        }
    }
}
