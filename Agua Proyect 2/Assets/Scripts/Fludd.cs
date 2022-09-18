using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fludd : MonoBehaviour
{
    public GameObject player;

    Vector3 v3Direction;

    public float fWater = 200;
    public float fChangePerSecond;
    public float JumpOffset = 5f;
    public float Hover;

    public float fHoverTime = 3f;

    public bool bInWater = false;


    void Start()
    {
        v3Direction = Vector3.up;
    }

    // Update is called once per frame
    void Update()
    {
        fWater = Mathf.Clamp(fWater, 0, 150);

        if (Input.GetKey(KeyCode.J) && bInWater == false && fWater > 0)
        {
            if (fHoverTime > 0)
            {
                fHoverTime -= Time.deltaTime;

                fWater -= fChangePerSecond * Time.deltaTime;

                player.transform.Translate(v3Direction * JumpOffset * Time.deltaTime);

                //player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + JumpOffset, player.transform.position.z);
            }

            else if (fHoverTime <= 0)

            {
                StartCoroutine(Wait());
            }
            
        }
        else if (Input.GetKey(KeyCode.J) && bInWater == false && fWater <= 0f)
        {
            Debug.Log("Damn");
        }

        if (Input.GetKey(KeyCode.K) && bInWater == true)
        {
            fWater += fChangePerSecond * Time.deltaTime;
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
