// A test character used for debugging purposes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : Character
{
    private void Start()
    {
        name = "Debug Helper";
        paths_to_images = new Dictionary<string, string>() {
            {"neutral", "Sprites/Debug/Debug.png"},
            {"happy", "Sprites/Debug/Debug.png"}
            };
    }
}
