// UI Manager: Has functions specific to the UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image background = null; // The game's background
    [SerializeField] private RectTransform background_transform = null; // The game's background's transform
    [SerializeField] private Image character = null; // The character's image 
    [SerializeField] private RectTransform character_transform = null; // The character's image's transform
    [SerializeField] private TextMeshProUGUI header_text = null; // Place where the character's name goes
    [SerializeField] private TextMeshProUGUI dialogue_text = null; // Place where the character's speech goes
    
    [SerializeField] private int reference_width = 1600; // How wide the image is for a base 16:9 resolution
    [SerializeField] private int reference_height = 840; // How high the image can be for a base 16:9 resolution
    
    // Swap image for background
    public void ChangeBackgroundImage(string background_path)
    {
        Sprite temp = Resources.Load<Sprite>(background_path);
        AdjustImageHeight(temp, background_transform);
        background.sprite = temp;
    }
    
    // Swap image for character
    public void ChangeCharacterImage(string character_path)
    {
        Sprite temp = Resources.Load<Sprite>(character_path);
        AdjustImageWidth(temp, character_transform);
        character.sprite = temp;
    }
    
    // Change the text shown in the header box
    public void ChangeCharacterText(string new_name)
    {
        header_text.text = new_name;
    }
    
    // Change the text shown in the dialogue box
    public IEnumerator ChangeDialogueText(string new_speech, bool is_thought=false)
    {
        do
        {
            if (!is_thought) dialogue_text.text = new_speech;
            else 
            {
                dialogue_text.text = "<i>" + new_speech + "</i>";
            }
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Mouse0));
    }
    
    public void ResetBoxes()
    {
        ChangeCharacterText("");
        dialogue_text.text = ""; // Directly set because otherwise it will be cleared after a mouse click
    }
    
    // Adjust the image's height so the background looks proportionally correct
    private void AdjustImageHeight(Sprite sprite, RectTransform transform)
    {
        transform.sizeDelta = new Vector2(reference_width, (reference_width*sprite.rect.height) / sprite.rect.width);
    }

    // Adjust the image's width
    private void AdjustImageWidth(Sprite sprite, RectTransform transform)
    {
        transform.sizeDelta = new Vector2 ((reference_height*sprite.rect.width) / sprite.rect.height, reference_height);
        //new Vector2((reference_width*sprite.rect.width) / sprite.rect.width, reference_height);
    }
}
