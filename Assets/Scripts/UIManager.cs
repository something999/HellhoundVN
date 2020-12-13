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
    [SerializeField] private GameObject back_button = null; // The button choice for displaying flipped choices
    [SerializeField] private GameObject chances = null; // UI that displays chances
    [SerializeField] private Image transition_screen = null; 
    [SerializeField] private TextMeshProUGUI message = null;
    [SerializeField] private string lose_message = "<b>An Unfortunate End</b>\nThe odds were not in your favor...";
    [SerializeField] private string win_message = "<b>Fortune Favors the Bold</b>\nCongratulations!\nZuckerbork enjoyed your reading.";
    [SerializeField] private int reference_width = 1600; // How wide the image is for a base 16:9 resolution
    [SerializeField] private int reference_height = 840; // How high the image can be for a base 16:9 resolution
    
    public void RemoveChance()
    {
        Destroy(chances.transform.GetChild(chances.transform.childCount - 1).gameObject);
    }
    
    // Fade in or fade out the game over screen
    public IEnumerator PlayTransition(float time, bool fadein, bool is_ending)
    {
        transition_screen.gameObject.SetActive(true);
        if (fadein)
        {
            yield return Fade(transition_screen, time, Color.black, new Color (0f, 0f, 0f, 0f));
        }
        else
        {
            yield return Fade(transition_screen, time, new Color(0f, 0f, 0f, 0f), Color.black);
        }
        if (!is_ending) transition_screen.gameObject.SetActive(false);
    }
    
    private IEnumerator Fade(Image image, float time, Color a, Color b)
    {
        float elapsed = 0f;
        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(a, b, elapsed/time);
            yield return null;
        }
    }
    
    public void ShowGameOver()
    {
        message.text = lose_message;
        foreach (Transform child in transition_screen.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
    public void ShowVictory()
    {
        transition_screen.gameObject.SetActive(true);
        message.text = win_message;
        transition_screen.transform.GetChild(0).gameObject.SetActive(true);
        transition_screen.transform.GetChild(transition_screen.transform.childCount-1).gameObject.SetActive(true);
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
        if (temp == null) Debug.Log(character_path);
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
    
    // Show the card UI
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
                    // StartCoroutine(Fade(card.GetComponent<Image>(), .5f, Color.white, new Color(0f, 0f, 0f, 0f)));
                    StartCoroutine(Fade(card.GetComponent<Image>(), .5f, new Color(0f, 0f, 0f, 0f), Color.white));
                    card.enabled = false;
                }
                else
                {
                    StartCoroutine(Fade(card.GetComponent<Image>(), .5f, new Color(0f, 0f, 0f, 0f), Color.white));
                }
            }
        }
        else
        {
            foreach (Transform child in cards.transform)
            {
                StartCoroutine(Fade(child.GetComponent<Image>(), .5f, Color.white, new Color(0f, 0f, 0f, 0f)));
                GameObject.Destroy(child.gameObject, 1f);
            }
        }    
    }
    
    // Reset all cards' states
    public void ResetCards()
    {
        foreach (Transform child in cards.transform)
        {
            child.gameObject.SetActive(true);
            StartCoroutine(Fade(child.GetComponent<Image>(), .5f, new Color(0f, 0f, 0f, 0f), Color.white));
            child.GetComponent<Button>().interactable = true;
            child.GetComponent<Button>().enabled = true;
        }
    }
    
    // Disable cards
    public void DisableCard(string name, bool active, bool fade_active_card, string name_of_middle_card)
    {
        foreach (Transform child in cards.transform)
        {
            if (child.name != name)
            {
                StartCoroutine(Fade(child.GetComponent<Image>(), .5f, Color.white, new Color(0f, 0f, 0f, 0f)));
                if (!active) 
                {
                    child.gameObject.SetActive(false);
                }
                child.GetComponent<Button>().interactable = false;
            }
            else
            {
                if (fade_active_card && name != name_of_middle_card) 
                {
                    StartCoroutine(Fade(child.GetComponent<Image>(), .5f, new Color(0f, 0f, 0f, 0f), Color.white));
                }
                child.GetComponent<Button>().enabled = false;
            }
        }
    }
    
    // Show buttons UI
    public void ShowButtons(bool display_buttons, bool display_flipped_button, bool display_back_button)
    {
        flipped_button.SetActive(display_flipped_button);
        back_button.SetActive(display_back_button);
        buttons.SetActive(display_buttons);
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
