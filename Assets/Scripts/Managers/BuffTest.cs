using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffTest : MonoBehaviour
{


    public float duration;
    public bool recoveryState;

    public Image fillImage;


    // Start is called before the first frame update
    void Start()
    {
        fillImage.fillAmount = 0f;
        recoveryState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recoveryState)
        {
        StartCoroutine(Timer(duration));
        }
    }




    private IEnumerator Timer(float duration)
    {
        recoveryState = false;
        float startTime = Time.time;
        float time = duration;
        float value = 0f;
        fillImage.fillAmount = 1f;

        while (Time.time - startTime < duration)
        {
            time -= Time.deltaTime;
            value = time/duration;
            fillImage.fillAmount = value;
            yield return null;
        }
    }
}
