// GameManager, which controls the timing of in-game events
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Inherits from parser so we can access the structures in Parser
public class GameManager : Parser
{
    [SerializeField] private GameObject card_display = null; // UI that displays the cards
    [SerializeField] private GameObject background = null; // UI that displays the game's background
    [SerializeField] private TextMeshProUGUI name_text = null; // UI text that displays the current speaking character's name
    [SerializeField] private TextMeshProUGUI dialogue_text = null; // UI text that displays the current speaking character's dialogue
     List<command> command_list;
    private void Start()
    {
       command_list =Parse("Assets/Resources/Texts/SampleDialogue.txt"); 
       StartCoroutine(PlayScene(command_list));
    }
    
    /*
        Read the list of commands and execute the instructions
    */
    private IEnumerator PlayScene(List<command> command_list)
    {
        foreach (command c in command_list)
        {
            switch (c.type)
            {
                case "scene": 
                    break;
                case "emotion": 
                    break;
                case "character":
                    ChangeCharacterText(c.arg);
                    break;
                 case "dialogue":
                    yield return ChangeDialogueText(c.arg);
                    break;
                 default:
                    break;
            }
        }
        ResetBoxes();
    }
    
    // Change text in the header (contains the character's name)
    private void ChangeCharacterText(string new_text)
    {
        name_text.text = new_text;
    }
    
    // Change text in the dialogue box
    private IEnumerator ChangeDialogueText(string new_text)
    {
        do
        {
            dialogue_text.text = new_text;
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Mouse0));
    }
    
    // Clear text from the header and dialogue box
    private void ResetBoxes()
    {
        ChangeCharacterText("");
        dialogue_text.text = ""; // Setting directly because otherwise it will only be enabled after a mouse click
    }
}
