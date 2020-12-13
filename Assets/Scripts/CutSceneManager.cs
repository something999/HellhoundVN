using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] private GameManager game;
    public IEnumerator PlayCutscene()
    {
        game.AddCommand("dialogue", "");
        game.AddCommand("clear", "");
        yield return StartCoroutine(game.PlayScene(game.GetCommands()));
    }
}
