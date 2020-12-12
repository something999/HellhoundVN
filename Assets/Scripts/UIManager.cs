// UI Manager: Has functions specific to the UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image background = null; // The game's background
    [SerializeField] private RectTransform background_transform = null; // The game's background
    [SerializeField] private TextMeshProUGUI header_text = null; // Place where the character's name goes
    [SerializeField] private TextMeshProUGUI dialogue_text = null; // Place where the character's speech goes
    
    [SerializeField] private int reference_width = 1600; // How wide the image is for a base 16:9 resolution
    
    // Swap image for background
    public void ChangeBackgroundImage(string background_path)
    {
        Sprite temp = Resources.Load<Sprite>(background_path);
        ResizeImage(temp);
        background.sprite = temp;
    }
    
    // Change the text shown in the header box
    public void ChangeCharacterText(string new_name)
    {
        header_text.text = new_name;
    }
    
    // Change the text shown in the dialogue box
    public IEnumerator ChangeDialogueText(string new_speech)
    {
        do
        {
            dialogue_text.text = new_speech;
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Mouse0));
    }
    
    public void ResetBoxes()
    {
        ChangeCharacterText("");
        dialogue_text.text = ""; // Directly set because otherwise it will be cleared after a mouse click
    }
    
    private void ResizeImage(Sprite sprite)
    {
        background_transform.sizeDelta = new Vector2(reference_width, (reference_width*sprite.rect.height) / sprite.rect.width);
    }
}
