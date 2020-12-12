// An object that reads and interprets the script
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{
    public void Start()
    {
        Parse("Assets/Resources/Texts/SampleDialogue.txt");
    }
    
    // I opted to separated each of these into four separate regular expression groups instead one huge regular expression for readability
    private Regex scene_format = new Regex("^SCENE - (?'scene'.*)$", RegexOptions.Compiled); // Regular expression for scenes
    private Regex character_format = new Regex("(?'character'.*?):", RegexOptions.Compiled); // Regular expression for character names
    private Regex emotion_format = new Regex("(?!\\B\"[^\"]*)\\((?'emotion'.*)\\)(?![^\"]*\"\\B)", RegexOptions.Compiled); // Regular expression for emotions
    private Regex dialogue_format = new Regex("(?'dialogue'\".*\")", RegexOptions.Compiled); // Regular expression for dialogue lines
    
    // Parses the instructions written in the filepath
    public void Parse(string filepath)
    {
        try
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    FindRegexMatch(line, scene_format, "scene");
                    FindRegexMatch(line, character_format, "character");
                    FindRegexMatch(line, emotion_format, "emotion");
                    FindRegexMatch(line, dialogue_format, "dialogue");
                }
                reader.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Parser: There was a problem reading " + filepath);
            Debug.LogError(e.Message);
        }
    }
    
    
    // Attempt to find a match according to this pattern
    private void FindRegexMatch(string line, Regex pattern, string group_name)
    {
        if (pattern.IsMatch(line))
        {
            GroupCollection groups = pattern.Match(line).Groups;
            Debug.Log(groups[group_name].Value);
        }
    }
}
