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
    
    // Move an object to create a parallax effect
    public void MoveObjectBasedOnMousePosition(GameObject object_to_move, Vector2 center_point, Vector2 mouse_position, float distance, float max_distance)
    {
       object_to_move.transform.position = new Vector2(center_point.x + Mathf.Clamp(mouse_position.x * distance,  -max_distance, max_distance), center_point.y + Mathf.Clamp(mouse_position.y * distance, -max_distance, max_distance));
    }
}
