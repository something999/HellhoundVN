// UI Manager: Has functions specific to the UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject cards = null; // Show the cards?
    [SerializeField] private Image background = null; // The game's background
    [SerializeField] private RectTransform background_transform = null; // The game's background's transform
    [SerializeField] private Image character = null; // The character's image 
    [SerializeField] private RectTransform character_transform = null; // The character's image's transform
    [SerializeField] private TextMeshProUGUI header_text = null; // Place where the character's name goes
    [SerializeField] private TextMeshProUGUI dialogue_text = null; // Place where the character's speech goes
    [SerializeField] private GameObject buttons = null; // Choice buttons
    [SerializeField] private GameObject flipped_button = null; 
    
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
    
    public void ShowCards(bool display, string[] cards_to_show=null)
    {
        if (display)
        {
            int start_position = -375;
            for (int i = 0; i < cards_to_show.Length; i++)
            {
                Button card = Instantiate(Resources.Load<Button>(cards_to_show[i]));
                card.transform.parent = cards.transform;
                card.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position + (375 * i), 100);
                card.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }  
        }
        else
        {
            foreach (Transform child in cards.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        cards.SetActive(display);
    }
    
    // Redundant code for moving cards -- fix in future
    public void ResetCards()
    {
        int start_position = -375;
        //foreach (Transform child in cards.transform)
        for (int i = 0; i < cards.transform.childCount; i++)
        {
            cards.transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position + (374 * i), 100);
            cards.transform.GetChild(i).GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            cards.transform.GetChild(i).GetComponent<Button>().interactable = true;
            cards.transform.GetChild(i).GetComponent<Button>().enabled = true;
        }
    }
    
    public void DisableCard(string name)
    {
        foreach (Transform child in cards.transform)
        {
            if (child.name != name) child.GetComponent<Button>().interactable = false;
            else child.GetComponent<Button>().enabled = false;
        }
    }
    
    public void ShowButtons(bool display_buttons, bool display_flipped_button)
    {
        if (display_flipped_button) flipped_button.SetActive(true);
        else flipped_button.SetActive(false);
        if (display_buttons) buttons.SetActive(true);
        else buttons.SetActive(false);
    }
    
    public void ResetAll()
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
