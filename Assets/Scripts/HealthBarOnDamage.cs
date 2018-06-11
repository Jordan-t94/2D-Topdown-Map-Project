using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(ObjectHealthBarController))]
public class HealthBarOnDamage : MonoBehaviour
{
    [SerializeField]
    private Canvas _healthCanvas;
    [SerializeField]
    private float _showLength = 5.0f;

    private HealthController _healthController;
    private ObjectHealthBarController _uiController;
    private bool _canvasEnabled;
    private bool _showHealthbar;
    private float _healthSinceLastCheck = 0;
    private float _timerAmount;

    public bool IsCanvasEnabled
    {
        get
        {
            return _canvasEnabled;
        }
        private set
        {
            _canvasEnabled = value;
        }
    }

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
        _uiController = GetComponent<ObjectHealthBarController>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (_showHealthbar)
        {
            _uiController.UpdateHealthSlider();

            if (HasHealthChanged())
            {
                _healthSinceLastCheck = _healthController.StatController.CurrentAmount;
                _timerAmount = 0.0f;
            }

            if (!_healthCanvas.enabled)
                ToggleHealthCanvas();

            _timerAmount += Time.deltaTime;

            if (_timerAmount >= _showLength)
            {
                _showHealthbar = false;
                _timerAmount = 0.0f;
            }
        }
        else
        {
            if (_healthCanvas.enabled)
                ToggleHealthCanvas();

            if (_healthSinceLastCheck == 0)
                _healthSinceLastCheck = _healthController.StatController.CurrentAmount;

            if (HasHealthChanged())
            {
                //Health has changed, show health bar
                _healthSinceLastCheck = _healthController.StatController.CurrentAmount;
                _showHealthbar = true;
            }
        }
    }

    private bool HasHealthChanged()
    {
        if (_healthController.StatController.CurrentAmount == _healthSinceLastCheck)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void ToggleHealthCanvas()
    {
        _canvasEnabled = !_canvasEnabled;
        _healthCanvas.enabled = _canvasEnabled;
    }
}
