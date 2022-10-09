using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStream : MonoBehaviour
{
    public Interruptor IA;

    public ParticleSystem particleSus;

    public Fludd fld;

    public WaterIndicator WI;

    public float fWaterGiven = 15f;

    public bool ifHBeingPressed = false;


    List<ParticleCollisionEvent> colevents = new List<ParticleCollisionEvent>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (fld.fWater == 0)
        {
            particleSus.Stop();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (fld.fWater > 0)
            {
                particleSus.Play();
                ifHBeingPressed = true;

            }
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
             particleSus.Stop();
             ifHBeingPressed = false;
        }
        


        if (ifHBeingPressed == true)
        {
            fld.fWater -= fld.fChangePerSecond * Time.deltaTime;
            WI.SetWater(fld.fWater);

        }
    }


    private void OnParticleCollision(GameObject other)
    {
        int events = particleSus.GetCollisionEvents(other, colevents);

        for (int i = 0; i < events; i++)
        {

        }

        /*if (other.gameObject.tag == "Interruptor")
        {
            IA.GiveWater(fWaterGiven);
            Debug.Log("hi");
        }*/

        if (other.TryGetComponent(out Interruptor en))
        {
            if (other.gameObject.tag == "Interruptor")
            {
                en.GiveWater(fWaterGiven);
                Debug.Log("hi");
            }
            if (other.gameObject.tag == "TimerInterruptor")
            {
                en.GiveWatertoTimed(fWaterGiven);
                Debug.Log("hi");
            }
        }     
    }
}
