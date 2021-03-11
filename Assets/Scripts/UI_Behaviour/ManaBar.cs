using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public static ManaBar staticBar;
    private Image ManaBarImage;

    private void Awake() {
        staticBar = this;
    }
    private void Start()
    {
        ManaBarImage = GetComponent<Image>();
    }

    public void SetValue(float value)
    {
        ManaBarImage.fillAmount = value;
    }

    public float GetValue()
    {
        return ManaBarImage.fillAmount;
    }

    public void IncreaseValue(float value)
    {
        ManaBarImage.fillAmount += value;
    }

    public void DecreaseValue(float value)
    {
        ManaBarImage.fillAmount -= value;
    }
}
