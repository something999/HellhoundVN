using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private static MouseManager mouse;
    [SerializeField] private Texture2D cursor_sprite = null;
    private void Awake()
    {
        if (mouse != null && mouse != this) Destroy (this.gameObject);
        else
        {

            Cursor.SetCursor(cursor_sprite, Vector2.zero, CursorMode.Auto);
            mouse=this;
        }
    }
}
