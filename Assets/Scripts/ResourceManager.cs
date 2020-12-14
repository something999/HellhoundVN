// Class that manages sprites
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<string, string> paths_to_backgrounds; // Documents where the background sprites are in the project hierarchy
    private Dictionary<string, string> paths_to_characters; // Documents where the character sprites are in the project hierarchy
    private Dictionary<string, string> paths_to_cards; // Documents where the card buttons are in the project hierarchy
    private Dictionary<string, string> paths_to_choice_results; // Documents where the choice results are stored in the project hierarchy
    
    private void Start()
    {
        paths_to_backgrounds = new Dictionary<string, string>() 
        {
            {"DEBUG", "Sprites/Debug/DebugBackground"},
            {"DARK", "Sprites/Backgrounds/Dark"},
            {"OFFICE", "Sprites/Backgrounds/Office"},
        };
        
        paths_to_characters = new Dictionary<string, string>()
        {
            {"DEBUG", "Sprites/Zuckerbork/ZuckerborkNeutral"},
            {"Disgust", "Sprites/Zuckerbork/ZuckerborkDisgusted"},
            {"Angry", "Sprites/Zuckerbork/ZuckerborkAngry"},
            {"Anger", "Sprites/Zuckerbork/ZuckerborkAngry"},
            {"Irritated", "Sprites/Zuckerbork/ZuckerborkIrritated"},
            {"Confused", "Sprites/Zuckerbork/ZuckerborkConfused"},
            {"Happy", "Sprites/Zuckerbork/ZuckerborkHappy"},
            {"Laugh", "Sprites/Zuckerbork/ZuckerborkLaughing"},
            {"Laughing", "Sprites/Zuckerbork/ZuckerborkLaughing"},
            {"Neutral", "Sprites/Zuckerbork/ZuckerborkNeutral"},
            {"Sadness", "Sprites/Zuckerbork/ZuckerborkSad"}
        };
        
        paths_to_cards = new Dictionary<string, string>()
        {
            {"DEBUG", "Cards/Debug/DebugCardCenter"},
            {"Sun", "Cards/Sun"},
            {"Devil", "Cards/Devil"},
            {"Hermit", "Cards/Hermit"},
        };
        
         paths_to_choice_results = new Dictionary<string, string>()
        {
            {"DEBUG", ""},
            {"Devilupright0", "Assets/Resources/Texts/Results/ZuckerborkDevilUpright-1.txt"}, // Devil upright - Part 1
            {"Sunupright0", "Assets/Resources/Texts/Results/ZuckerborkSunUpright-1.txt"}, // Sun upright - Part 1
            {"Hermitupright0", "Assets/Resources/Texts/Results/ZuckerborkHermitUpright-1.txt"}, // Hermit upright - Part 1
            {"Hermitupright1", "Assets/Resources/Texts/Results/ZuckerborkHermitUpright-2.txt"}, // Hermit upright - Part 2
            {"Hermitflipped1", "Assets/Resources/Texts/Results/ZuckerborkHermitReversed-1.txt"}, // Hermit reversed - Part 2
            {"Sunupright1", "Assets/Resources/Texts/Results/ZuckerborkSunUpright-2.txt"}, // Sun upright - Part 2
            {"Sunflipped1", "Assets/Resources/Texts/Results/ZuckerborkSunReversed-1.txt"}, // Sun reversed - Part 2
            {"Sunupright2", "Assets/Resources/Texts/Results/ZuckerborkSunUpright-3.txt"}, // Sun upright - Part 3
            {"Sunflipped2", "Assets/Resources/Texts/Results/ZuckerborkSunReversed-2.txt"} // Sun reversed - Part 3
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
    
    // Get path to the choice's result. If it doesn't exist, log an error.
    public string GetChoicePath(string choice_name)
    {
        if (!paths_to_choice_results.ContainsKey(choice_name))
        {
            Debug.LogWarning("ResourceManager does not contain results for " + choice_name);
            return paths_to_choice_results["DEBUG"];
        }
        return paths_to_choice_results[choice_name];
    }
}
