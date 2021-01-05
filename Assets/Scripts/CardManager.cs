// Handles the card display
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    [SerializeField] private GameObject card_display = null; // The GameObject that the cards will be parented to
    [SerializeField] private string card_folder_path = null; // The path to the folder that holds the cards in the project hierarchy
    [SerializeField] private float fade_duration = 0f; // How long does it take for a card to fade-in or fade-out?
    
    private string[] last_known_cards = null; // The last known cards displayed on-screen
    private bool last_known_card_state = false; // The last known card state (interactable / not interactable)
    
    private UIEffectManager ui_effect_manager = null; // Needed to fade-in the cards
    
    // Initialize UIEffectManager
    private void Awake()
    {
        ui_effect_manager = this.GetComponent<UIEffectManager>();
    }
    
    // Display a set of cards on screen
    public IEnumerator ShowCards (string[] card_names)
    {
        foreach (string c in card_names)
        {
            Button card = Instantiate(Resources.Load<Button>(card_folder_path + c));
            if (card != null)
            {
                card.transform.SetParent(card_display.transform, false);
            }
        }
        EnableCards(false); // Don't allow a player to click on a card while the effect occurs
        yield return StartCoroutine(ui_effect_manager.FadeImage(card_display.transform, fade_duration, new Color(0f, 0f, 0f, 0f), Color.white, false));
        card_display.SetActive(true);
        RecordCardNames(card_names); // Needed for the back button --> otherwise we don't know what cards we presented last time.
    }
    
    // Determines whether the player can interact with the cards
    public void EnableCards (bool make_cards_interactable)
    {
        foreach (Transform child in card_display.transform)
        {
            child.GetComponent<Button>().enabled = make_cards_interactable;
        }
        RecordInteractivity(make_cards_interactable);
    }
    
    // Display a set of cards on-screen based on previously known information
    // Do NOT use this version of the method if you want to present a new set of choices -- only the previously recorded set is displayed
    public void ShowCards ()
    {
        StartCoroutine(ShowCards(last_known_cards));
        EnableCards(last_known_card_state);
    }
    
    // Remove all cards from the screen
    public void ResetCards ()
    {
         foreach (Transform child in card_display.transform)
         {
            GameObject.Destroy(child.gameObject);
        }
        card_display.SetActive(false);        
    }
    
    // Records information about the cards and whether they were interactive or not 
    // This is information is being used for method overloading
    private void RecordCardNames (string[] card_names)
    {
        last_known_cards = card_names;
    }
    
    // Records information about the cards and whether they were interactive or not 
    // This is information is being used for method overloading
    private void RecordInteractivity (bool make_cards_interactable)
    {
        last_known_card_state = make_cards_interactable;
    }
    
    // Focus on the selected card (UI Effect)
    public IEnumerator FocusOnSelectedCard (GameObject card)
    {
        yield return StartCoroutine(ui_effect_manager.FadeImage(card_display.transform, fade_duration, new Color(0f, 0f, 0f, 0f), Color.white, true));
        foreach (Transform child in card_display.transform)
        {
            if (child.gameObject != card) child.gameObject.SetActive(false);
        }
        yield return StartCoroutine(ui_effect_manager.FadeImage(card.GetComponent<Image>(), fade_duration, new Color(0f, 0f, 0f, 0f), Color.white, false));
        yield return null;
    }
}