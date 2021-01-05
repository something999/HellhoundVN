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
    private SpriteManager sprite_manager = null;
    
    private bool cutscene_enabled = false; 
    private string last_recorded_cutscene = "";
    
    private void Awake()
    {
        content_parser = this.GetComponent<ContentParser>();
        dialogue_box_manager = this.GetComponent<DialogueBoxManager>();
        card_manager = this.GetComponent<CardManager>();
        game_manager = this.GetComponent<GameManager>();
        sprite_manager = this.GetComponent<SpriteManager>();
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
                    sprite_manager.UpdateBackground(scene);
                } 
                if (!string.IsNullOrEmpty(emotion)) 
                {
                    sprite_manager.UpdateCharacter(game_manager.GetCurrentHound(), emotion);
                }
                if (!string.IsNullOrEmpty(cutscene))
                {
                    /*yield return StartCoroutine(card_manager.ShowCards(Regex.Split(cutscene.Replace(" ", string.Empty), "\\|\\|")));
                    card_manager.EnableCards(false);
                    card_manager.DisableCardDisplayBackground(true);*/
                    SaveCutscene(cutscene);
                    cutscene_enabled = true;
                }
                if (!string.IsNullOrEmpty(choices)) 
                {
                    dialogue_box_manager.ClearBox();
                    yield return StartCoroutine(card_manager.ShowCards(Regex.Split(choices.Replace(" ", string.Empty), "\\|\\|")));
                    card_manager.EnableCards(true);
                    continue;
                }
                if (!string.IsNullOrEmpty(character)) dialogue_box_manager.UpdateCharacterName(character);
                if (!string.IsNullOrEmpty(dialogue))  
                {
                    if (cutscene_enabled)
                    {
                        yield return StartCoroutine(card_manager.ShowCards(Regex.Split(last_recorded_cutscene.Replace(" ", string.Empty), "\\|\\|")));
                        card_manager.EnableCards(false);
                        card_manager.DisableCardDisplayBackground(true);
                    }
                    yield return StartCoroutine(dialogue_box_manager.UpdateDialogue(dialogue));
                    if (cutscene_enabled)
                    {
                        card_manager.ResetCards();
                        card_manager.DisableCardDisplayBackground(false);
                        cutscene_enabled = false;
                    }
                };
            }
        dialogue_box_manager.ClearBox();
    }

    public IEnumerator ProcessContent(string[] file_names, int file_line_index)
    {
        foreach (string file in file_names)
        {
            yield return StartCoroutine(ProcessContent(file, file_line_index));
        }
    }
    
    // Save information about the cutscene so we care fire it with the next line of dialogue
    private void SaveCutscene(string cutscene)
    {
        last_recorded_cutscene = cutscene;
    }
}

