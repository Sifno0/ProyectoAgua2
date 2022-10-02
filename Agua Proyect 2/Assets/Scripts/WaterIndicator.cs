using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterIndicator : MonoBehaviour
{
    public Slider slider;

    public void setMaxWater(float water)
    {
        slider.maxValue = water;
        slider.value = water;
    }

    public void SetWater(float water)
    {
        slider.value = water;
    }
}
