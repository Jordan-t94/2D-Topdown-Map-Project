using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairSpriteChanger : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] _crosshairTextures;
    [SerializeField]
    private Image _crosshairPreview;

    private MouseCursorTexture _cursorTextureController;
    private Texture2D _currentCrosshair;

    private void Awake()
    {
        _cursorTextureController = GameController.Instance.GetComponent<MouseCursorTexture>();
        _crosshairTextures = GameController.Instance.CrosshairSprites;

        _currentCrosshair = _cursorTextureController.CursorTexture;
        SetPreviewSprite(_currentCrosshair);
    }

    private void SetPreviewSprite(Texture2D crosshair)
    {
        _crosshairPreview.sprite = Sprite.Create(crosshair, new Rect(0, 0, crosshair.width, crosshair.height), new Vector2(0.5f, 0.5f));
        _crosshairPreview.rectTransform.sizeDelta = new Vector2(64f, 64f);
    }
}
