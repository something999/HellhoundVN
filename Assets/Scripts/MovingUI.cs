/*
    Moving UI
    Script for moving UI elements (such as the background)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUI : MonoBehaviour
{
    [SerializeField] private UIEffectManager ui_effect_manager = null; 
    [SerializeField] private float distance = 0f; // How far this element should move
    [SerializeField] private float max_distance = 0f; // How far the element can move
    
    private Vector2 center_point; // Where the object is located relative to the scene at the start of the game
    
    private void Start()
    {
        center_point = this.transform.position;
    }
    
    private void Update()
    {
        if (ui_effect_manager != null)
        {
            Vector2 mouse_position = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            ui_effect_manager.MoveObjectBasedOnMousePosition(this.gameObject, center_point, mouse_position, distance, max_distance);
        }
    }
}
