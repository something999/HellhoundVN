using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string[] main_dialogue_filenames; // Contains all dialogue file locations
    [SerializeField] private string[] correct_card_names; // Contains the names of the correct cards
    [SerializeField] private string[] order_of_hounds; // The order of the hounds
    [SerializeField] private int[] hound_checkpoint_counts; // Each hound has a number of checkpoints --> use this to keep track of failures. The length of hound_checkpoint_counts should be the same as order_of_hounds.
    [SerializeField] private string failure_message_folder; // Where the failure messages are stored
    [SerializeField] private string ending_folder; // Where the endings are stored
    [SerializeField] private string good_ending_file; // Where the "good" ending is stored 
    [SerializeField] private string bad_ending_file; // Where the normal "bad" ending is stored
    [SerializeField] private string bad_ending_file_2; // Where the variant "bad" ending is stored
    [SerializeField] private int number_of_chances = 3; // The max number of chances we have before it's game-over
    
    private ContentManager content_manager = null;
    private ButtonManager button_manager = null;
    private CardManager card_manager = null;
    private CutsceneManager cutscene_manager = null;
    
    private bool allow_reversed = false; // Allows the player to present  a card reversed
    private int file_index = 0; // Which file are we starting with 
    private int card_index = 0; // Which answer are we checking
    private int hound_index = 0; // Which hound we're looking at
    private int hound_checkpoint = 0; // Which index are we looking at
    private int mistake_checkpoint = 0; // How many wrong choices have we made in the game?
    private int repeat_checkpoint = 0; // Are we repeating the same choice?
    
    private string last_chosen_card = ""; // The name of the previously chosen card
    private string current_chosen_card = ""; // The name of the card chosen
    
    private void Start()
    {
        content_manager = this.GetComponent<ContentManager>();
        button_manager = this.GetComponent<ButtonManager>();
        card_manager = this.GetComponent<CardManager>();
        cutscene_manager = this.GetComponent<CutsceneManager>();
        StartCoroutine(PlayIntro());
    }
    
    // Play the introduction sequence
    public IEnumerator PlayIntro ()
    {
        yield return StartCoroutine(content_manager.ProcessContent(main_dialogue_filenames[file_index], 0));
        yield return StartCoroutine(cutscene_manager.ShowTransitionScreen());
        file_index += 1;
        yield return StartCoroutine(content_manager.ProcessContent(main_dialogue_filenames[file_index], 0));
    }
    
    // Reveal the available cards
    public IEnumerator ShowCardChoices(string path_to_upright, string path_to_reversed)
    {
        card_manager.EnableCards(false);
        yield return StartCoroutine(content_manager.ProcessContent(path_to_upright, 0));
        if (allow_reversed) yield return StartCoroutine(content_manager.ProcessContent(path_to_reversed, 0));
        button_manager.ShowButtons(true, allow_reversed);
    } 
    
    // Record a card's name so we can check it later
    public void RecordCardName(string card_name)
    {
        current_chosen_card = card_name;
    }
    
    // Check the card and make sure it's correct
    public IEnumerator CheckCard (string card_state)
    {
        card_manager.ResetCards();
        button_manager.ShowButtons(false, false);
        // Example: ZuckerborkSunUpright0
        string player_response = order_of_hounds[hound_index] + current_chosen_card + card_state + hound_checkpoint;
        if (player_response == correct_card_names[card_index])
        {
            yield return StartCoroutine(PlayNextMessage());
            yield return null;
        }
        else if (repeat_checkpoint + mistake_checkpoint + 1 > number_of_chances)
        {
            if (last_chosen_card == current_chosen_card) yield return StartCoroutine(content_manager.ProcessContent(new string[] {ending_folder + bad_ending_file_2, ending_folder + bad_ending_file}, 0));
            else yield return StartCoroutine(content_manager.ProcessContent(ending_folder + bad_ending_file, 0));
            cutscene_manager.PlayBadEnding();
        }
        else yield return StartCoroutine(PlayFailureMessage(player_response));
        last_chosen_card = current_chosen_card;
    }
    
    // If the choice was right, play the next piece of dialogue
    private IEnumerator PlayNextMessage()
    {
        file_index += 1;
        if (file_index >= main_dialogue_filenames.Length - 1)
        {
            yield return StartCoroutine(content_manager.ProcessContent(ending_folder + good_ending_file, 0));
            cutscene_manager.PlayGoodEnding();
        }
        else
        {
            StartCoroutine(content_manager.ProcessContent(main_dialogue_filenames[file_index], 0));
            UpdateCheckpoint();
        }
    }
    
    // If the choice was wrong, play the unique failure message for that card
    private IEnumerator PlayFailureMessage(string state)
    {
        // Check if this is a situation where we chose the same card again
        if (last_chosen_card == current_chosen_card)
        {
            yield return StartCoroutine(content_manager.ProcessContent(new string[] {failure_message_folder + state + "a", failure_message_folder + order_of_hounds[hound_index] + "Repeat" + repeat_checkpoint}, 0));
            repeat_checkpoint += 1;
        }
        else
        {
            yield return StartCoroutine(content_manager.ProcessContent(new string[] {failure_message_folder + state + "a", failure_message_folder + state + "b", failure_message_folder + order_of_hounds[hound_index] + "Mistake" + mistake_checkpoint}, 0));
            mistake_checkpoint += 1;
        }
        button_manager.GoBackToCards();
    }
    
    // Update the checkpoints so that they match the flow of the story
    private void UpdateCheckpoint()
    {
        if (hound_checkpoint > hound_checkpoint_counts[hound_index])
        {
            if (hound_index < order_of_hounds.Length - 1)
            {
                hound_checkpoint = 0;
                mistake_checkpoint = 0;
                repeat_checkpoint = 0;
                last_chosen_card = "";
                hound_index += 1;
            }
        }
        else 
        {
            hound_checkpoint += 1;
        }
    }
}
