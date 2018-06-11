using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatController
{
    [Header("Stat")]

    [SerializeField]
    private float _maxAmount = 100.0f;

    [Header("Drain Stats")]

    [SerializeField]
    private float _drainAmount = 3.0f;
    [SerializeField]
    private float _drainRate = 0.5f;

    [Header("Regeneration Stats")]

    [SerializeField]
    private bool _canRegenerate = true;
    [SerializeField]
    private float _regenCooldown = 2.0f;
    [SerializeField]
    private float _regenAmount = 3.0f;
    [SerializeField]
    private float _regenRate = 0.4f;

    private float _currentAmount = 100.0f;
    private bool _isCurrentlyUsing = false;
    private bool _needToCooldown = false;
    private float _drainTimer = 0.0f;
    private float _regenTimer = 0.0f;
    private float _waitTimer = 0.0f;

    public bool IsCurrentlyUsing
    {
        get
        {
            return _isCurrentlyUsing;
        }
        private set
        {
            _isCurrentlyUsing = value;
        }
    }

    public float CurrentAmount
    {
        get
        {
            return _currentAmount;
        }

        set
        {
            _currentAmount = value;

            if (_currentAmount > _maxAmount)
                _currentAmount = _maxAmount;

            if (_currentAmount < 0)
                _currentAmount = 0;

            if (!_needToCooldown)
                _needToCooldown = true;
        }
    }

    public float MaxAmount
    {
        get
        {
            return _maxAmount;
        }
        private set
        {
            _maxAmount = value;
        }
    }

    public void Initialize()
    {
        _currentAmount = _maxAmount;
    }

    public void Update()
    {
        if (_needToCooldown)
        {
            WaitForCooldown();
        }
        else
        {
            Regenerate();
        }
    }
    public void Update(string buttonName)
    {
        if (Input.GetButton(buttonName) && _currentAmount != 0 && _currentAmount >= _drainAmount)
        {
            _isCurrentlyUsing = true;

            Drain();
        }
        else
        {
            _isCurrentlyUsing = false;

            if (_needToCooldown)
            {
                WaitForCooldown();
            }
            else
            {
                Regenerate();
            }
        }
    }
    public void Update(string buttonName, Vector3 movement)
    {
        if (Input.GetButton(buttonName) && _currentAmount != 0 && _currentAmount >= _drainAmount && movement != Vector3.zero)
        {
            _isCurrentlyUsing = true;

            Drain();
        }
        else
        {
            _isCurrentlyUsing = false;

            if (_needToCooldown)
            {
                WaitForCooldown();
            }
            else
            {
                Regenerate();
            }
        }
    }

    private void Drain()
    {
        if (!_needToCooldown)
            _needToCooldown = true;

        _drainTimer += Time.deltaTime;

        if (_drainTimer >= _drainRate)
        {
            _currentAmount -= _drainAmount;
            _drainTimer = 0.0f;
        }

        if (_currentAmount < 0)
        {
            _currentAmount = 0;
        }
    }

    private void WaitForCooldown()
    {
        if (_waitTimer < _regenCooldown)
        {
            _waitTimer += Time.deltaTime;
        }
        else if (_waitTimer >= _regenCooldown)
        {
            _waitTimer = 0.0f;
            _needToCooldown = false;
        }
    }

    private void Regenerate()
    {
        if (_canRegenerate && _currentAmount != _maxAmount)
        {
            if (_regenTimer < _regenRate)
            {
                _regenTimer += Time.deltaTime;
            }
            else if (_regenTimer >= _regenRate)
            {
                _currentAmount += _regenAmount;
                _regenTimer = 0.0f;
            }

            if (_currentAmount > _maxAmount)
                _currentAmount = _maxAmount;
        }
    }
}
