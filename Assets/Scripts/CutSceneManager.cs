/*
    CutsceneManager
    Handles the intro transition and game-over screens
    Current Endings:
        - Bad Ending (Make too many mistakes)
        - Good Ending
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Image transition_background = null; // The background of the game-over screen
    [SerializeField] private TextMeshProUGUI game_over_title = null; // What the game-over screen says
    [SerializeField] private Button quit_button = null; // Reference to the quit button
    [SerializeField] private TextMeshProUGUI button_label = null; // What the button says
    [SerializeField] private float transition_duration = 0f; // How long the transition screen fade-in lasts
    
    private UIEffectManager ui_effect_manager = null; // Needed to fade in the transition screen
    private Color original_color = Color.white; // The original color of the background
    
    private void Awake()
    {
        ui_effect_manager = this.GetComponent<UIEffectManager>();
        original_color = transition_background.color;
    }
    
    // Show the intro 
    public IEnumerator ShowTransitionScreen()
    {
        yield return StartCoroutine(ui_effect_manager.FadeImage(transition_background, transition_duration, original_color, new Color(0f, 0f, 0f, 0f), false));
        transition_background.gameObject.SetActive(false); // Need to disable this object or else we can't interact with the cards
    }
    
    // Show the game over screen
    private IEnumerator ShowGameOverScreen()
    {
        yield return StartCoroutine(ui_effect_manager.FadeImage(transition_background, transition_duration, transition_background.color, original_color, false));
        game_over_title.gameObject.SetActive(true);
        quit_button.gameObject.SetActive(true);
    }
    
    // Show the good ending
    public void PlayGoodEnding()
    {
        transition_background.gameObject.SetActive(true);
        game_over_title.text =  "<b><size=120%><font=\"SawarabiGothic\">Fortune Favors the Bold</b></font>\n<size=85%>Zuckerbork enjoyed your reading, but you still have a long way to go.</size>";
        button_label.text = "To be continued another time";
        StartCoroutine(ShowGameOverScreen());
    }
    
    // Show the bad ending
    public void PlayBadEnding()
    {
        transition_background.gameObject.SetActive(true);
        game_over_title.text = "<b><size=120%><font=\"SawarabiGothic\">A Tragic Demise</b></font>\n<size=85%>You died, but at least you satiated a hound's hunger.</size>";
        button_label.text = "The end";
        StartCoroutine(ShowGameOverScreen());
    }
}
