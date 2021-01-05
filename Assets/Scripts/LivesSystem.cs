/*
    Lives System
    Updates the UI to show how many lives (also referred to in other scripts as chances) are left
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesSystem : MonoBehaviour
{
    [SerializeField] private GameObject lives_ui = null;
    
    public void RemoveLife()
    {
        int number_of_children = lives_ui.transform.childCount; 
        if (number_of_children > 0) Destroy(lives_ui.transform.GetChild(number_of_children - 1).gameObject);
    }
}
