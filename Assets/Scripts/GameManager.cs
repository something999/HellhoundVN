// GameManager, which controls the timing of in-game events
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Inherits from parser so we can access the structures in Parser
public class GameManager : Parser
{
    [SerializeField] private SpriteManager sprite_manager = null; // Reference to the SpriteManager script
    [SerializeField] private UIManager ui = null; // Reference to the UIManager script
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
                    ui.ChangeBackgroundImage(sprite_manager.GetPath(c.arg));
                    break;
                case "emotion": 
                    break;
                case "character":
                    ui.ChangeCharacterText(c.arg);
                    break;
                 case "dialogue":
                    yield return ui.ChangeDialogueText(c.arg);
                    break;
                 default:
                    break;
            }
        }
        ui.ResetBoxes();
    }
}
