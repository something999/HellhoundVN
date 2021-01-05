/*
    Content Manager
    Processes a .txt file
*/
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField][MinAttribute(0)] private int temp_size = 128; // Represents the max size of the content holder
    
    private ContentParser content_parser = null;
    private DialogueBoxManager dialogue_box_manager = null;
    private CardManager card_manager = null;
    private GameManager game_manager = null;
    
    private void Awake()
    {
        content_parser = this.GetComponent<ContentParser>();
        dialogue_box_manager = this.GetComponent<DialogueBoxManager>();
        card_manager = this.GetComponent<CardManager>();
        game_manager = this.GetComponent<GameManager>();
    }
    
    // Travels through the dialogue line-by-line and processes information
    // public IEnumerator ProcessContent(string[] file_names, int file_index, int file_line_index)
    public IEnumerator ProcessContent(string file_name, int file_line_index)
    {
        string[] temp_lines = new string[temp_size];
        // for (int i = file_index; i < file_names.Length; ++i)
        // {
            // temp_lines = content_parser.GetContent(file_names[i]);
            temp_lines = content_parser.GetContent(file_name);
            foreach (string line in temp_lines)
            {
                // If the line is null or empty, there's nothing to match
                if (string.IsNullOrEmpty(line)) continue;
                // Chose to cache results so we don't call functions multiple times
                string scene = content_parser.GetScene(line);
                string character = content_parser.GetCharacter(line);
                string dialogue = content_parser.GetDialogue(line);
                string emotion = content_parser.GetEmotion(line);
                string choices = content_parser.GetChoices(line);
                string cutscene = content_parser.GetCutscene(line);
                
                // Assume that a match was found
                if (!string.IsNullOrEmpty(scene)) 
                {
                    
                } // Load background
                if (!string.IsNullOrEmpty(cutscene))
                {
                    yield return StartCoroutine(card_manager.ShowCards(Regex.Split(cutscene.Replace(" ", string.Empty), "\\|\\|")));
                    card_manager.EnableCards(false);
                    yield return StartCoroutine(dialogue_box_manager.UpdateDialogue(""));
                    card_manager.ResetCards();
                }
                if (!string.IsNullOrEmpty(choices)) 
                {
                    yield return StartCoroutine(card_manager.ShowCards(Regex.Split(choices.Replace(" ", string.Empty), "\\|\\|")));
                    card_manager.EnableCards(true);
                    continue;
                } // Load choices
                if (!string.IsNullOrEmpty(character)) dialogue_box_manager.UpdateCharacterName(character);
                if (!string.IsNullOrEmpty(dialogue))  yield return StartCoroutine(dialogue_box_manager.UpdateDialogue(dialogue));
                if (!string.IsNullOrEmpty(emotion)) {} // Load emotions
            }
        // }
        dialogue_box_manager.ClearBox();
    }

    public IEnumerator ProcessContent(string[] file_names, int file_line_index)
    {
        foreach (string file in file_names)
        {
            yield return StartCoroutine(ProcessContent(file, file_line_index));
        }
    }
}

