using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private bool _isAlive;

    [Header("Health Settings")]
    [SerializeField]
    private bool _hasDamageRateCooldown;
    [SerializeField]
    private float _takeDamageRate = 0.25f;

    [SerializeField]
    private StatController _healthStatController;

    private bool _canBeDamaged = true;
    private float timer = 0.0f;

    public StatController StatController
    {
        get
        {
            return _healthStatController;
        }
        private set
        {
            _healthStatController = value;
        }
    }

    private void Awake()
    {
        _isAlive = true;
        _healthStatController.Initialize();
    }

    public void ReduceHealth(float healthAmount)
    {
        if (_canBeDamaged)
            _healthStatController.CurrentAmount -= healthAmount;
    }

    public void GainHealth(float healthAmount)
    {
        _healthStatController.CurrentAmount += healthAmount;
    }

    private void Update()
    {
        _healthStatController.Update();
        UpdateDamageRateTimer();
        CheckIfDead();
    }

    private void UpdateDamageRateTimer()
    {
        if (_hasDamageRateCooldown)
        {
            if (timer < _takeDamageRate)
            {
                if (_canBeDamaged)
                    _canBeDamaged = false;

                timer += Time.deltaTime;
            }

            if (timer >= _takeDamageRate && !_canBeDamaged)
            {
                timer = 0.0f;
                _canBeDamaged = true;
            }
        }
    }

    private void CheckIfDead()
    {
        if (_healthStatController.CurrentAmount <= 0)
        {
            _isAlive = false;
        }

        if (!_isAlive)
        {
            Debug.Log("Destroying GameObject: " + gameObject.name);
            Destroy(gameObject);
        }
    }
}
