using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerIndicator : MonoBehaviour
{
    public Slider sliderTimer;

    public void setMaxTime(float timer)
    {
        sliderTimer.maxValue = timer;
        sliderTimer.value = timer;
    }

    public void SetTime(float timer)
    {
        sliderTimer.value = timer;
    }
}
