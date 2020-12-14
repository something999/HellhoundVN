using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pause_menu = null;
    private bool enable_pause_menu = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu(enable_pause_menu);
        }
    }
    
    private void ShowPauseMenu(bool display)
    {
        pause_menu.SetActive(display);
        if (display) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        enable_pause_menu = !enable_pause_menu;
        GameObject.FindObjectOfType<UIManager>().GetComponent<UIManager>().UpdatePaused(enable_pause_menu);
    }
}
