/*
    Dialogue Box Manager
    Updates information in the dialogue box, such as the character's name or speech
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueBoxManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name_box = null; // Where the character's name is displayed in the UI
    [SerializeField] private TextMeshProUGUI dialogue_box = null; // Where the character's speech is displayed in the UI
    
    // Update the character name displayed in the text box
    public void UpdateCharacterName (string name)
    {
        name_box.text = name;
    }
    
    // Update dialogue
    public IEnumerator UpdateDialogue(string speech)
    {
        do
        {
            dialogue_box.text = speech;
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1)); // To be later updated with a keybinding of the user's choice.
    }
    
    // Clear the dialogue box of names and speeches
    public void ClearBox()
    {
        UpdateCharacterName("");
        dialogue_box.text = "";
    }
}
