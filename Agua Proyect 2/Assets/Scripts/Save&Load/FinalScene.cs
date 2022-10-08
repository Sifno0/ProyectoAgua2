using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    public Text NameBox;

    void Start ()
    {
        NameBox.text = PlayerPrefs.GetString("name");
    }
}
