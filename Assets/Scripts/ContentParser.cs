/*
    Content Parser
    Gets information from a .txt. file and returns the content stored inside it
*/
using System.Text.RegularExpressions; // Necessary for Regex
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentParser : MonoBehaviour
{
    /*
        Script formatting notes:
        <<This is where you put a scene name (no quotes allowed)>>
        Character name goes here: "Dialogue is here." [Emotions are here]
        ((Choices are indiciated like this || and separated like this))
        {{Cutscene cards are indiciated like this || and separated like this}} --> This syntax will show cards on-screen but disable their interactivity.
    */
    private Regex scene_regex = new Regex("^<<(?!(:|\"))(?'scene'.*)>>$", RegexOptions.Compiled);
    private Regex char_regex = new Regex("^(?'character'.*?):", RegexOptions.Compiled);
    private Regex dialogue_regex = new Regex("(\"|\')(?'dialogue'.*)(\"|\')", RegexOptions.Compiled);
    private Regex emotion_regex = new Regex("(?!\\B\"[^\"]*)\\[(?'emotion'.*)\\](?![^\"]*\"\\B)", RegexOptions.Compiled);
    private Regex choice_regex = new Regex("^\\(\\((?!(:|\"))(?'choice'.*)\\)\\)$", RegexOptions.Compiled);
    private Regex cutscene_regex = new Regex("^{{(?!(:|\"))(?'cutscene'.*)}}$");
    
    // Returns the lines listed in a .txt file
    // We assume that valid paths refer to locations relative to Resources, not Assets
    public string[] GetContent (string file_name)
    {
        TextAsset text = Resources.Load<TextAsset>(file_name);
        try
        {
            return Regex.Split(text.text, "\n|\r|\r\n");
        }
        catch
        {
            Debug.LogError("Warning: ContentParser failed to read " + file_name);
            return new string[0];
        }
    }
    
    public string GetScene (string line)
    {
        return FindRegexMatch(line, scene_regex, "scene");
    }
    
    public string GetCharacter (string line)
    {
        return FindRegexMatch(line, char_regex, "character");
    }
    
    public string GetDialogue (string line)
    {
        return FindRegexMatch(line, dialogue_regex, "dialogue");
    }
    
    public string GetEmotion (string line)
    {
        return FindRegexMatch(line, emotion_regex, "emotion");
    }
    
    public string GetChoices (string line)
    {
        return FindRegexMatch(line, choice_regex, "choice");
    }
    
    public string GetCutscene (string line)
    {
        return FindRegexMatch(line, cutscene_regex, "cutscene");
    }
    
    private string FindRegexMatch(string line, Regex pattern, string group_name)
    {
        if (!pattern.IsMatch(line)) return "";
        GroupCollection groups = pattern.Match(line).Groups;
        return groups[group_name].Value;
    }        
}
