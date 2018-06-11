using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField]
    private Image _healthSlider;
    [SerializeField]
    private Image _energySlider;

    private PlayerMovement _playerMovement;
    private HealthController _healthController;
    private StatController _sprintController;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _healthController = GetComponent<HealthController>();
        _sprintController = _playerMovement.SprintController;
    }

    private void Update()
    {
        UpdateSlider(_energySlider, _sprintController);
        UpdateSlider(_healthSlider, _healthController.StatController);
    }

    private void UpdateSlider(Image slider, StatController stat)
    {
        float amount = 1 / (stat.MaxAmount / stat.CurrentAmount);

        if (slider.fillAmount != amount)
            slider.fillAmount = amount;
    }
}
