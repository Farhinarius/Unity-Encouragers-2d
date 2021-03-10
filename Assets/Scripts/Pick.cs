using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    TextMesh textRenderer;

    void Start()
    {
        textRenderer = transform.Find("Selection").GetComponent<TextMesh>();
        Color c = textRenderer.color;
        c.a = 0;
        textRenderer.color = c;
    }

    private void OnMouseEnter() {
        StartCoroutine(FadeIn());
    }

    private void OnMouseExit() {
        StartCoroutine(FadeOut());
    }

    private void OnMouseDown() {
        GameManager.instance.SetPlayerName(this.name);
        GameManager.instance.LoadNextPlayableScene();
    }

    IEnumerator FadeIn()
    {
        for (float ft = 0; ft <= 1f; ft += 0.04f)
        {
            Color c = textRenderer.color;
            c.a = ft;
            textRenderer.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOut()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.04f)
        {
            Color c = textRenderer.color;
            c.a = ft;
            textRenderer.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
