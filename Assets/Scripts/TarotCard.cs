/*
    Tarot card class
    Holds information about a tarot card (name, meaning, etc.)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarotCard : MonoBehaviour
{
    [SerializeField] private string name_of_card = ""; // The name of the card
    [SerializeField] private string card_meaning_folder = ""; // Where the files with the card's meanings are stored
    // The current filename format for cards is [Name of Card] + Upright / Reversed. To avoid clogging up the Inspector, the values of the file names are initialized in Awake, but if you wanted to change the file names, you could add the [SerializeField] attribute instead.
    private string card_upright_meaning_file = "";
    private string card_reversed_meaning_file = "";
    
    private GameManager game_manager = null; // Need this reference in order to show card meanings in the dialogue box
    private CardManager card_manager = null; // Need this reference to do a UI effect
    
    private void Awake ()
    {
        game_manager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        card_manager = GameObject.FindObjectOfType<CardManager>().GetComponent<CardManager>();
        card_upright_meaning_file = name_of_card + "Upright";
        card_reversed_meaning_file = name_of_card + "Reversed";
    }
    
    // Represents the player selecting this card
    public void ChooseCard ()
    {
        StartCoroutine(ShowCardMeaning());
    }
    
    // Reveal the card's meaning in the dialogue box
    private IEnumerator ShowCardMeaning()
    {
        game_manager.RecordCardName(name_of_card);
        yield return StartCoroutine(card_manager.FocusOnSelectedCard(this.gameObject));
        yield return StartCoroutine(game_manager.ShowCardChoices(card_meaning_folder + card_upright_meaning_file, card_meaning_folder +  card_reversed_meaning_file));
    }
}
