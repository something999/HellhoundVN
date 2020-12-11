// An object representing a person with an in-game speaking role
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected string name; // The name of the character
    protected Dictionary<string, string> paths_to_images; // Paths to this character's images -- the keys represent shorthands (ex. "happy", "sad", "angry")
    
    // Returns the string representing the path to this sprite. The path should be contained in the Resources folder.
    public string GetSprite(string key)
    {
        if (!paths_to_images.ContainsKey(key)) Debug.LogError("Character: " + key + " is not present for the character " + name); // Throw error if the emotion is not found in the dictionary
        return paths_to_images[key];
    }
}
