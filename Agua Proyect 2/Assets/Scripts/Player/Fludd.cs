using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fludd : MonoBehaviour
{
    public WaterIndicator WI;

    public Movement PM;

    public GameObject player;

    public ParticleSystem particleomega;
    public ParticleSystem particleomega2;
    

    public Vector3 v3Direction;

    public float fWater = 150;
    public float fChangePerSecond;
    public float JumpOffset = 5f;

    public float fHoverTime = 3f;

    public bool bInWater = false;



    void Start()
    {
        v3Direction = Vector3.up;

        WI.setMaxWater(fWater);
    }

    // Update is called once per frame
    void Update()
    {
        if (fWater == 0)
        {
            particleomega.Stop();
            particleomega2.Stop();
        }

        fWater = Mathf.Clamp(fWater, 0, 150);

        if (Input.GetKey(KeyCode.J) && bInWater == false  )
        {
            if (fHoverTime > 0 &&  fWater > 0)
            {
                
                fHoverTime -= Time.deltaTime;

                fWater -= fChangePerSecond * Time.deltaTime;

                WI.SetWater(fWater);

                //player.transform.Translate(v3Direction * JumpOffset * Time.deltaTime);

                //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + JumpOffset, player.transform.position.z);



                Debug.Log("yee");
            }
            else if (fHoverTime <= 0)

            {
                StartCoroutine(Wait());
            }            
        }
        if (Input.GetKeyDown(KeyCode.J) && fWater > 0)
        {
            particleomega.Play();
            particleomega2.Play();
            

        }
        else if (Input.GetKeyUp(KeyCode.J) && fWater > 0)
        {
            particleomega.Stop();
            particleomega2.Stop();
            
        }

        if (Input.GetKey(KeyCode.K) && bInWater == true)
        {

            WI.SetWater(fWater);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);

        fHoverTime = 3f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            bInWater = true;

            Debug.Log("Yes");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            bInWater = false;  
            
            Debug.Log("No");
        }
    }

    
}
