using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] private ResourceManager resources;
    [SerializeField] private string[] answers;
    // int checkpoint = 0;
    
    public void SetChoiceStandard()
    {
        StartCoroutine(CheckChoice(game.selected_card + "upright"));
    }
    
    public void SetChoiceFlipped()
    {
        StartCoroutine(CheckChoice(game.selected_card + "flipped"));
    }
    
    // private void CheckChoice(string choice)
    private IEnumerator CheckChoice(string choice)
    {
        game.AddCommand("clear", "");
        game.AddCommand(resources.GetChoicePath(choice + game.GetCheckpoint()));  
        yield return StartCoroutine(game.PlayScene(game.GetCommands()));
        if (game.CheckAnswer(choice))
        {
            game.AddCommand(game.GetNextPart());
            if (game.GetCheckpoint() == 3) game.AddCommand("victory", "");
            yield return StartCoroutine(game.PlayScene(game.GetCommands()));
        }
        else
        {
            if (game.CheckStatus())
            {
                game.AddCommand("Texts/Acts/ZuckerborkFailure.txt");
                game.AddCommand("ending", "");
            }
            else
            {
                if (game.GetCheckpoint() == 0) game.AddCommand("choice", "Sun,Devil,Hermit");
                else if (game.GetCheckpoint() == 1) game.AddCommand("choice", "Sun,Hermit");
                else game.AddCommand("choice","Sun");
            }
            yield return StartCoroutine(game.PlayScene(game.GetCommands()));
        }
    }
}
