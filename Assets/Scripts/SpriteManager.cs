// Class that manages sprites
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private Dictionary<string, string> paths_to_backgrounds; // Documents where the background sprites are in the project hierarchy
    private Dictionary<string, string> paths_to_characters; // Documents where the character sprites are in the project hierarchy
    private void Start()
    {
        paths_to_backgrounds = new Dictionary<string, string>() 
        {
            {"DEBUG", "Sprites/Debug/DebugBackground"}
        };
        
        paths_to_characters = new Dictionary<string, string>()
        {
            {"DEBUG", "Sprites/Debug/DebugCharacter"}
        };
    }
    
    // Get path to the background. If it doesn't exist, replace with a placeholder image.
    public string GetBackgroundPath(string sprite_name)
    {
        if (!paths_to_backgrounds.ContainsKey(sprite_name))
        {
            Debug.LogWarning("SpriteManager does not contain the image " + sprite_name + ". Replacing with debug image.");
            return paths_to_backgrounds["DEBUG"];
        }
        return paths_to_backgrounds[sprite_name];
    }
    
    // Get path to the character's image. If it doesn't exist, replace with a placeholder image.
    public string GetCharacterPath(string sprite_name)
    {
        if (!paths_to_characters.ContainsKey(sprite_name))
        {
            Debug.LogWarning("SpriteManager does not contain the image " + sprite_name + ". Replacing with debug image.");
            return paths_to_characters["DEBUG"];
        }
        return paths_to_characters[sprite_name];
    }
}
