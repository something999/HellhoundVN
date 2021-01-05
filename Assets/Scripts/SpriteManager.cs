/*
    Sprite Manager
    Handles tasks related to sprite swapping
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private string background_image_folder; // The folder where background sprites are stored
    [SerializeField] private string character_image_folder; // The folder where character sprites are stored
    [SerializeField] private Image background_image; // The background image object
    [SerializeField] private Image character_image; // The character image object
    // Update the background image
    public void UpdateBackground(string background_image_name)
    {
        Sprite bg = Resources.Load<Sprite>(background_image_folder + background_image_name);
        if (bg != null) background_image.sprite = bg;
    }
    
    // Update the character image
    public void UpdateCharacter(string character, string emotion_image_name)
    {
        Sprite ch = Resources.Load<Sprite>(character_image_folder + character + "/" + emotion_image_name);
        if (ch != null) character_image.sprite = ch;
    }
}
