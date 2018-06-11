using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthController))]
public class ObjectHealthBarController : MonoBehaviour
{
    [SerializeField]
    private Image _healthSliderImage;

    private HealthController _healthController;

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();
    }

    public void UpdateHealthSlider()
    {
        UpdateSlider(_healthSliderImage, _healthController.StatController);
    }

    private void UpdateSlider(Image slider, StatController stat)
    {
        float amount = 1 / (stat.MaxAmount / stat.CurrentAmount);

        if (slider.fillAmount != amount)
            slider.fillAmount = amount;
    }
}
