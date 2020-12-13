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
    [SerializeField] private CutSceneManager cutscene_manager = null; // Reference to the SpriteManager script
    [SerializeField] private UIManager ui = null; // Reference to the UIManager script
    private string[] answers; // Answers at the various stages of the game
    private string[] acts; // References to the script, which are meant to be executed in-order (excludes choices)
    private int checkpoint = 0; // Where in answers do we need to check
    
    public bool show_standard = true;
    public bool show_flipped = false;
    public string selected_card = "";
    [SerializeField] private int chances = 3; // The number of chances the player has before it's game over
    
    private List<command> command_list;
     
    private void Start()
    {
       // command_list =Parse("Assets/Resources/Texts/SampleDialogue.txt"); 
       // command_list=Parse("Assets/Resources/Texts/ZuckerborkOpening.txt"); 
       // StartCoroutine(PlayScene(command_list));
       answers = new string[]
       {
           "devilupright"
       };
       acts = new string[]
       {
           "Assets/Resources/Texts/Debug/SampleDialogue.txt", 
           "Assets/Resources/Texts/Acts/ZuckerborkPart3.txt"
           //"Assets/Resources/Texts/Acts/ZuckerborkPart1.txt",
           //"Assets/Resources/Texts/Acts/ZuckerborkPart2.txt"
       };
       command_list = Parse(acts[checkpoint]);
       StartCoroutine(PlayScene(command_list));
    }
    
    /*
        Read the list of commands and execute the instructions
    */
    public IEnumerator PlayScene(List<command> command_list)
    {
        foreach (command c in command_list)
        {
            switch (c.type)
            {
                case "clear":
                    ClearChoices();
                    break;
                case "cutscene":
                    StartCoroutine(cutscene_manager.PlayCutscene());
                    break;
                case "buttons":
                    ui.ShowButtons(true, show_flipped);
                    break;
                case "show":
                    ShowChoices(c.arg, false);
                    break;                    
                case "choice":
                    ShowChoices(c.arg);
                    break;
                case "move":
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
    
    public List<command> GetCommands()
    {
        return command_list;
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
    
    //Proceed to the next part
    public string GetNextPart()
    {
        checkpoint += 1;
        return acts[checkpoint];
    }
    
    // Show all the available choices
    private void ShowChoices(string line, bool enable = true)
    {
        Debug.Log(line);
        string[] choices = line.Split(',');
        for (int i = 0; i < choices.Length; i++)
        {
            choices[i] = resource_manager.GetButtonPath(choices[i]);
        }
        if (!enable) ui.ShowCards(true, choices, false);
        else ui.ShowCards(true, choices);
    }
    
    // Clear the choice screens
    public void ClearChoices()
    {
        ui.ShowCards(false);
        ui.ShowButtons(false, false);
    }
    
    public IEnumerator PlayChoice()
    {
       yield return StartCoroutine(PlayScene(command_list));  
       AddCommand("dialogue", "Should I present this card?");
       AddCommand("buttons", "");
       yield return StartCoroutine(PlayScene(command_list));
    }
    
    public void ReshowCards()
    {
        ui.ResetCards();
        ui.ShowButtons(false, false);
    }
    
    public bool CheckAnswer(string choice)
    {
        return choice.Equals(answers[checkpoint]);
    }
}
