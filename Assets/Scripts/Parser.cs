// An object that reads text files and translates them into commands for the GameManager
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
        Parse("Assets/Resources/Texts/SampleDialogue.txt"); // Debug statement --> Remove once GameManager is finished
    }
   
   // Represents a command (a change that's supposed to happen in the scene)
    private struct command
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
    private struct regular_expression
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
            new regular_expression(new Regex("^SCENE - (?'scene'.*)$", RegexOptions.Compiled), "scene"),
            new regular_expression(new Regex("(?'character'.*?):", RegexOptions.Compiled), "character"),
            new regular_expression(new Regex("(?!\\B\"[^\"]*)\\((?'emotion'.*)\\)(?![^\"]*\"\\B)", RegexOptions.Compiled), "emotion"),
            new regular_expression(new Regex("(?'dialogue'\".*\")", RegexOptions.Compiled), "dialogue")
        };
    
    // Parses the instructions written in the filepath
    public ArrayList Parse(string filepath)
    {
        ArrayList command_list = new ArrayList();
        try
        {
            using (StreamReader reader = new StreamReader(filepath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (regular_expression r in regex_list)
                    {
                        command? cmd = FindRegexMatch(line, r.pattern, r.group_name);
                        CheckCommand(cmd, command_list);
                    }
                }
                reader.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Parser: There was a problem reading " + filepath);
            Debug.LogError(e.Message);
        }
        return command_list;
    }
   
   // Check that there was a match against the pattern. If yes, document this match in the command_list.
   private void CheckCommand(command? cmd, ArrayList command_list)
   {
       if (cmd.HasValue) command_list.Add(cmd);
   }
   
    // Attempt to find a match according to this pattern
    private command? FindRegexMatch(string line, Regex pattern, string group_name)
    {
        if (pattern.IsMatch(line))
        {
            GroupCollection groups = pattern.Match(line).Groups;
            return new command(group_name, groups[group_name].Value);
        }
        return null;
    }
}
