using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorTexture : MonoBehaviour
{
    [SerializeField]
    private Texture2D _cursorTexture;
    [SerializeField]
    private CursorMode _cursorMode = CursorMode.Auto;
    [SerializeField]
    private Vector2 _hotSpot;

    public Texture2D CursorTexture
    {
        get
        {
            return _cursorTexture;
        }

        private set
        {
            _cursorTexture = value;
        }
    }

    private void Awake()
    {
        EnableMouseTexture();
    }

    private void EnableMouseTexture()
    {
        Cursor.SetCursor(CursorTexture, _hotSpot, _cursorMode);
    }

    private void DisableMouseTexture()
    {
        Cursor.SetCursor(null, Vector2.zero, _cursorMode);
    }
}
