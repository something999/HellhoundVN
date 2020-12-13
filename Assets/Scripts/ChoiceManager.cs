﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] private ResourceManager resources;
    [SerializeField] private string[] answers;
    int checkpoint = 0;
    
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
        game.AddCommand(resources.GetChoicePath(choice + checkpoint));  
        yield return StartCoroutine(game.PlayScene(game.GetCommands()));
        if (game.CheckAnswer(choice))
        {
            game.AddCommand(game.GetNextPart());
            yield return StartCoroutine(game.PlayScene(game.GetCommands()));
        }
        else
        {
            game.AddCommand("choice", "Sun,Devil,Hermit");
            yield return StartCoroutine(game.PlayScene(game.GetCommands()));
            Debug.Log("incorrect");
        }
    }
}