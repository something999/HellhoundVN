// Class that manages sprites
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    private Dictionary<string, string> paths_to_sprites; // Documents where the sprites are in the project hierarchy
    private void Start()
    {
        paths_to_sprites = new Dictionary<string, string>() 
        {
            {"DEBUG", "Sprites/Debug/debug"}
        };
    }
    public string GetPath(string sprite_name)
    {
        if (!paths_to_sprites.ContainsKey(sprite_name))
        {
            Debug.LogWarning("SpriteManager does not contain the image " + sprite_name + ". Replacing with debug image.");
            return paths_to_sprites["DEBUG"];
        }
        return paths_to_sprites[sprite_name];
    }
}
