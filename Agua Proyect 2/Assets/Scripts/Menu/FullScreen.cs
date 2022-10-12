using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FullScreen : MonoBehaviour
{
    public Toggle toggle;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolution;

    // Start is called before the first frame update
    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        CheckResolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFullScreen(bool FullScreen)
    {
        Screen.fullScreen = FullScreen;
    }

    public void CheckResolution()
    {
        resolution = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resolution.Length; i++)
        {
            string option = resolution[i].width + " x " + resolution[i].height;
            options.Add(option);

            if (resolution[i].width == Screen.currentResolution.width && resolution[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolucionActual;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void ChangeResolution(int ResolutionIndex)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolutionDropdown.value);

        Resolution resolucion = resolution[ResolutionIndex];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
    }
}
