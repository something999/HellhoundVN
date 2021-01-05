/*
    UI Effect Manager
    Handles various UI effects, such as fade-ins or parallax
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEffectManager : MonoBehaviour
{
    // Fade an image in or out
    public IEnumerator FadeImage (Image image, float total_time, Color a, Color b, bool reverse)
    {
        if (reverse)
        {
            Color tmp = a;
            a = b;
            b = tmp;
        }
        float elapsed_time = 0; // How much time has passed
        while (elapsed_time < total_time)
        {
            elapsed_time += Time.deltaTime;
            if (image != null) image.color = Color.Lerp(a, b, elapsed_time / total_time);
            yield return null;
        }
    }
    
    // Fade a list of images in or out
    public IEnumerator FadeImage (Transform transform, float total_time, Color a, Color b, bool reverse)
    {
        foreach (Transform child in transform)
        {
            StartCoroutine(FadeImage(child.GetComponent<Image>(), total_time, a, b, reverse));
        }
        yield return null;
    }
}
