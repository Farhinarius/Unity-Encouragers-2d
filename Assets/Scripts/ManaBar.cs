using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public static ManaBar staticBar;
    private Image ManaBarImage;

    private void Start()
    {
        ManaBarImage = GetComponent<Image>();
        staticBar = this;
    }

    public void SetManaBarValue(float value)
    {
        ManaBarImage.fillAmount = value;
    }

    public float GetManaBarValue()
    {
        return ManaBarImage.fillAmount;
    }
}
