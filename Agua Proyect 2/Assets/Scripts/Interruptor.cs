using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interruptor : MonoBehaviour
{
    public TimerIndicator TI;

    public Renderer materialB;

    public float fFireInContainer = 100;
    public float fFireInTimedContainer = 100;
    public float fTimedContainerStart;
    public float fTimedContainer;
    
    public GameObject GOa;
    public GameObject GOb;
    public GameObject CanvasTimer;


    private void Start()
    {
        fTimedContainer = fTimedContainerStart;
    }

    private void Update()
    {
        if (fFireInContainer <= 0)
        {
            GOa.SetActive(false);
            GOb.SetActive(false);

            materialB.material.color = Color.blue;
        }

        if(fFireInTimedContainer <= 0)
        {
            if(fTimedContainer > 0)
            {
                CanvasTimer.SetActive(true);

                fTimedContainer -= Time.deltaTime;

                TI.SetTime(fTimedContainer);

                materialB.material.color = Color.blue;

                GOa.SetActive(false);
                GOb.SetActive(false);
            }
            else if (fTimedContainer <= 0)
            {
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);

        CanvasTimer.SetActive(false);

        materialB.material.color = Color.red;

        fTimedContainer = fTimedContainerStart;

        TI.SetTime(fTimedContainer);

        fFireInTimedContainer = 100;

        GOa.SetActive(true);
        GOb.SetActive(true);
    }

    public void GiveWater(float water)
    {
        fFireInContainer -= water;
    }

    public void GiveWatertoTimed(float water)
    {
        fFireInTimedContainer -= water;
    }
}
