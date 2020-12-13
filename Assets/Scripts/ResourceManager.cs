// Class that manages sprites
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, string> paths_to_backgrounds; // Documents where the background sprites are in the project hierarchy
    private Dictionary<string, string> paths_to_characters; // Documents where the character sprites are in the project hierarchy
    private Dictionary<string, string> paths_to_cards; // Documents where the card buttons are in the project hierarchy
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
        
        paths_to_cards = new Dictionary<string, string>()
        {
            {"DEBUG", "Cards/DebugCardCenter"},
            {"Sun", "Cards/DebugCardLeft"},
            {"Hermit", "Cards/DebugCardCenter"},
            {"Devil", "Cards/DebugCardRight"},
        };
    }
    
    // Get path to the background. If it doesn't exist, replace with a placeholder image.
    public string GetBackgroundPath(string sprite_name)
    {
        if (!paths_to_backgrounds.ContainsKey(sprite_name))
        {
            Debug.LogWarning("ResourceManager does not contain the image " + sprite_name + ". Replacing with debug image.");
            return paths_to_backgrounds["DEBUG"];
        }
        return paths_to_backgrounds[sprite_name];
    }
    
    // Get path to the character's image. If it doesn't exist, replace with a placeholder image.
    public string GetCharacterPath(string sprite_name)
    {
        if (!paths_to_characters.ContainsKey(sprite_name))
        {
            Debug.LogWarning("ResourceManager does not contain the image " + sprite_name + ". Replacing with debug image.");
            return paths_to_characters["DEBUG"];
        }
        return paths_to_characters[sprite_name];
    }
    
    // Get path to the card's button. If it doesn't exist, replace with placeholder button.
    public string GetButtonPath(string button_name)
    {
        if (!paths_to_cards.ContainsKey(button_name))
        {
            Debug.LogWarning("ResourceManager does not contain the button " + button_name + ". Replacing with debug button.");
            return paths_to_cards["DEBUG"];
        }
        return paths_to_cards[button_name];
    }
}
