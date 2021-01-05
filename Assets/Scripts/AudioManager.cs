/*
    Audio Manager
    Ensures that the audio source doesn't get deleted when we change scenes
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    private void Awake()
    {
        if (instance == null) 
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else Destroy(this.gameObject);
    }
}
