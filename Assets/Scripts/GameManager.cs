// GameManager, which controls the timing of in-game events
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Inherits from parser so we can access the structures in Parser
public class GameManager : Parser
{
    [SerializeField] private ResourceManager resource_manager = null; // Reference to the SpriteManager script
    [SerializeField] private UIManager ui = null; // Reference to the UIManager script
    [SerializeField] private string[] answers; // Answers at the various stages of the game
    private int checkpoint = 0; // Where in answers do we need to check
    
    public bool show_standard = true;
    public bool show_flipped = false;
    public string selected_card = "";
    
    private List<command> command_list;
     
    private void Start()
    {
       // command_list =Parse("Assets/Resources/Texts/SampleDialogue.txt"); 
       command_list =Parse("Assets/Resources/Texts/ZuckerborkOpening.txt"); 
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
                case "buttons":
                    ui.ShowButtons(true, show_flipped);
                    break;
                case "choice":
                    ShowChoices(c.arg);
                    break;
                case "move":
                    GameObject.Find(c.arg).transform.localPosition = new Vector3(0f, 100f, 0f);
                    ui.DisableCard(c.arg);
                    break;
                case "scene": 
                    ui.ChangeBackgroundImage(resource_manager.GetBackgroundPath(c.arg));
                    break;
                case "emotion": 
                    ui.ChangeCharacterImage(resource_manager.GetCharacterPath(c.arg));
                    break;
                case "character":
                    ui.ChangeCharacterText(c.arg);
                    break;
                 case "dialogue":
                    yield return ui.ChangeDialogueText(c.arg);
                    break;
                 case "thought":
                    yield return ui.ChangeDialogueText(c.arg, true);
                    break;
                 default:
                    break;
            }
        }
        ui.ResetAll();
        ClearCommands();
    }
    
    // Empty the command list
    public void ClearCommands()
    {
        command_list.Clear();
    }
    
    // Add a command
    public void AddCommand(string type, string arg)
    {
        command_list.Add(new command(type, arg));
    }
    
    // Add a command by parsing the file at the designated filepath
    public void AddCommand(string filepath)
    {
        command_list.AddRange(Parse(filepath));
    }
    
    // Show all the available choices
    private void ShowChoices(string line)
    {
        string[] choices = line.Split(',');
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i] = resource_manager.GetButtonPath(choices[i]);
        }
        ui.ShowCards(true, choices);
    }
    
    public IEnumerator PlayChoice()
    {
       yield return StartCoroutine(PlayScene(command_list));  
       AddCommand("dialogue", "Should I present this card?");
       AddCommand("buttons", "");
       yield return StartCoroutine(PlayScene(command_list));
    }
    
    public void CheckChoiceUpright()
    {
        Debug.Log(selected_card + " upright" == answers[checkpoint]);
    }
    
    public void CheckChoiceFlipped()
    {
        Debug.Log(selected_card + " flipped" == answers[checkpoint]);
    }
    
    private void PlaySuccessMessage()
    {
        if (checkpoint == 0)
        {
            
        }
    }
    
    private void PlayFailureMessage()
    {
    }
    
    public void ReshowCards()
    {
        ui.ResetCards();
        ui.ShowButtons(false, false);
    }
}
