using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterStream : MonoBehaviour
{
    public ParticleSystem particleSus;
    List<ParticleCollisionEvent> colevents = new List<ParticleCollisionEvent>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            particleSus.Play();
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            particleSus.Stop();
        }     

    }


    private void OnParticleCollision(GameObject other)
    {
        int events = particleSus.GetCollisionEvents(other, colevents);

        for (int i = 0; i < events; i++)
        {

        }
    }
}
