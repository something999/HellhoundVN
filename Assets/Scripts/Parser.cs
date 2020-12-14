// An object that reads text files and translates them into commands for the GameManager
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser : MonoBehaviour
{  
   // Represents a command (a change that's supposed to happen in the scene)
    public struct command
    {
        public string type; // The type of command (scene, character, emotion, or dialogue)
        public string arg; // The argument for that command (ex. the name of the scene to load, the name of the character to reference, etc.)
        public command (string type, string arg)
        {
            this.type = type;
            this.arg = arg;
        }
    };
    
    // Represents a regular expression pattern (regex + a group name that is used to retrieve the arguments for the command)
    public struct regular_expression
    {
        public Regex pattern; // The regular expression pattern to use
        public string group_name; // The name to search for
        public regular_expression(Regex pattern, string group_name)
        {
            this.pattern = pattern;
            this.group_name = group_name;
        }
    }
    
    // I opted to make four separate regular expression groups instead one huge regular expression for readability
    private regular_expression[] regex_list = new regular_expression[]
        {
            new regular_expression(new Regex("^(?'clear'CLEAR*)$", RegexOptions.Compiled), "clear"), // Used for clearing choices / ending cutscenes
            new regular_expression(new Regex("^(?'transition'TRANSITION)$", RegexOptions.Compiled), "transition"), // Used for transitions
            new regular_expression(new Regex("^CHOICE-(?'choice'.*)$", RegexOptions.Compiled), "choice"), // Used for showing choices
            new regular_expression(new Regex("^SHOW-(?'show'.*)$", RegexOptions.Compiled), "show"), // Used for showing cutscenes
            new regular_expression(new Regex("^SCENE - (?'scene'.*)$", RegexOptions.Compiled), "scene"), // Background
            new regular_expression(new Regex("(?'character'.*?):", RegexOptions.Compiled), "character"), // Character
            new regular_expression(new Regex("(?!\\B\"[^\"]*)\\((?'position'.*)\\)(?![^\"]*\"\\B)", RegexOptions.Compiled), "position"), // Emotion (image for character)
            new regular_expression(new Regex("(?!\\B\"[^\"]*)\\[(?'emotion'.*)\\](?![^\"]*\"\\B)", RegexOptions.Compiled), "emotion"), // Emotion (image for character)
            new regular_expression(new Regex("\"(?'dialogue'.*)\"", RegexOptions.Compiled), "dialogue"), // Dialogue
            new regular_expression(new Regex("\'(?'thought'.*)\'", RegexOptions.Compiled), "thought") // Dialogue
        };
    
    // Parses the instructions written in the filepath
    public List<command> Parse(string filepath)
    {
        TextAsset TextDialogue = Resources.Load<TextAsset>(filepath);
        Debug.Log(filepath);
        string[] TextLines = TextDialogue.text.Split(System.Environment.NewLine.ToCharArray());

        List<command> command_list = new List<command>();
        try {
            foreach (string LineCurrent in TextLines)   {
                foreach (regular_expression r in regex_list)
                {
                    RecordRegexMatch(LineCurrent, r.pattern, r.group_name, command_list);
                    // CheckCommand(cmd, command_list);
                }
            }
        }

        catch (Exception e)
        {
            Debug.LogError("Parser: There was a problem reading " + filepath);
            Debug.LogError(e.Message);
        }
        return command_list;
    }
   
    // Attempt to find a match according to this pattern
    private void RecordRegexMatch(string line, Regex pattern, string group_name, List<command> command_list)
    {
        if (pattern.IsMatch(line))
        {
            GroupCollection groups = pattern.Match(line).Groups;
            command cmd = new command(group_name, groups[group_name].Value);
            command_list.Add(cmd);
        }
    }
}
