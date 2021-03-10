using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    // Start is called before the first frame update
    TextMesh textRenderer;
    void Start()
    {
        textRenderer = GetComponent<TextMesh>();
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.035f)
        {
            Color c = textRenderer.color;
            c.a = ft;
            textRenderer.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        for (float ft = 0; ft <= 1f; ft += 0.035f)
        {
            Color c = textRenderer.color;
            c.a = ft;
            textRenderer.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(FadeOut());
    }
}
