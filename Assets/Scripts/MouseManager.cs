using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursor_sprite = null;
    private void Awake()
    {
        Cursor.SetCursor(cursor_sprite, Vector2.zero, CursorMode.Auto);
    }
}
