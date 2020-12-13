// Class representing a card
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private string card_name; // The card's name
    [SerializeField] private string explanation_standard; // Where the card's explanation is stored
    [SerializeField] private string explanation_reversed; // Where the card's explanation reversed is stored
    private GameManager game;
    
    private void Start()
    {
        game = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    
    // Get the card's name
    public string GetCardName()
    {
        return card_name;
    }
    
    public void ReadMeaning()
    {
        GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>().DisableCard(card_name+"(Clone)", true);
        game.selected_card = card_name;
        game.AddCommand("character", "Acacia");
        if (game.show_standard)
        {
            game.AddCommand(explanation_standard);
        }
        if (game.show_flipped)
        {
            game.AddCommand(explanation_reversed);
        }
        game.AddCommand("move", this.gameObject.name);
        StartCoroutine(game.PlayChoice());
    }
}
