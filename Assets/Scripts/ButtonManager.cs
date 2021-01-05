/*
    Button Manager
    Controls which buttons are displayed on-screen
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject button_display = null; // The object that displays the presentation buttons.
    [SerializeField] private GameObject reversed_button = null; // The button that allows the player to display a card reversed
    
    private CardManager card_manager = null;
    private GameManager game_manager = null;
    
    private void Awake()
    {
        card_manager = this.GetComponent<CardManager>();
        game_manager = this.GetComponent<GameManager>();
    }
    
    // Display the buttons on-screen
    public void ShowButtons(bool show_buttons, bool show_reversed)
    {
        reversed_button.SetActive(show_reversed);
        button_display.SetActive(show_buttons);
    }
    
    // Button that allows the player to present the card in its upright position
    public void PresentCardUpright()
    {
        StartCoroutine(game_manager.CheckCard("Upright"));
    }
    
    // Button that allows the player to present the card in its reversed position
    public void PresentCardReversed()
    {
        StartCoroutine(game_manager.CheckCard("Reversed"));
    }
    
    // Back button function: Return to the card selection screen
    public void GoBackToCards()
    {
        card_manager.ResetCards();
        card_manager.ShowCards();
        card_manager.EnableCards(true);
        ShowButtons(false, false);
    }
}
