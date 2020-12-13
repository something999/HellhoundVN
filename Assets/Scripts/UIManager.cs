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
    [SerializeField] private GameObject flipped_button = null; // The button choice for displaying flipped choices
    [SerializeField] private GameObject chances = null; // UI that displays chances
    [SerializeField] private Image transition_screen = null; 
    
    [SerializeField] private int reference_width = 1600; // How wide the image is for a base 16:9 resolution
    [SerializeField] private int reference_height = 840; // How high the image can be for a base 16:9 resolution
    
    public void RemoveChance()
    {
        Destroy(chances.transform.GetChild(chances.transform.childCount - 1).gameObject);
    }
    
    // Fade in or fade out the game over screen
    public IEnumerator Fade(float time, bool fadein)
    {
        transition_screen.gameObject.SetActive(true);
        if (fadein)
        {
            float elapsed = 0f; // How much time elapsed
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                transition_screen.color = Color.Lerp(new Color(0f, 0f, 0f, 0f), Color.black, elapsed/time);
                yield return null;
            }
        }
        else
        {
            float elapsed = 0f;
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                transition_screen.color = Color.Lerp(Color.black, new Color(0f, 0f, 0f, 0f), elapsed/time);
                yield return null;
            }
            transition_screen.gameObject.SetActive(false);
        }
    }
    
    public void ShowGameOver()
    {
        foreach (Transform child in transition_screen.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
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
                dialogue_text.text = "(" + new_speech + ")";
            }
            yield return null;
        } while (!Input.GetKeyDown(KeyCode.Mouse0));
    }
    
    public void ShowCards(bool display, string[] cards_to_show=null, bool enable=true)
    {
        if (display)
        {
            foreach(string c in cards_to_show)
            {
                Button card = Instantiate(Resources.Load<Button>(c));
                card.transform.SetParent(cards.transform, false);
                if (!enable)
                {
                    card.enabled = false;
                }
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
    
    public void ResetCards()
    {
        foreach (Transform child in cards.transform)
        {
            child.gameObject.SetActive(true);
            child.GetComponent<Button>().interactable = true;
            child.GetComponent<Button>().enabled = true;
        }
    }
    
    public void DisableCard(string name, bool active = false)
    {
        foreach (Transform child in cards.transform)
        {
            if (child.name != name)
            {
                if (!active) child.gameObject.SetActive(false);
                child.GetComponent<Button>().interactable = false;
            }
            else
            {
                child.GetComponent<Button>().enabled = false;
            }
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
    }
}
