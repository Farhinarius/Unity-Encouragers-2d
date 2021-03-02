using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image HealthBarImage;

    private void Start()
    {
        HealthBarImage = GetComponent<Image>();
    }

    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

}
