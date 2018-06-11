using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _damage = 10.0f;
    [SerializeField]
    private float _aliveTime = 2.0f;
    [SerializeField]
    private LayerMask _collideWithLayers;

    private TrailRenderer _trailRend;
    private float _timeAlive = 0.0f;

    private void Awake()
    {
        _trailRend = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        CheckLifespan();

        Vector3 newPos = transform.up * _speed * Time.deltaTime;

        if (!GridChecker.IsPositionAllowed(transform.position + newPos))
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.position += newPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_collideWithLayers.value >> collision.gameObject.layer) == 1)
        {
            HealthController healthController = collision.gameObject.GetComponent<HealthController>();
            if (healthController != null)
            {
                healthController.ReduceHealth(_damage);
                gameObject.SetActive(false);
            }
        }
    }

    private void CheckLifespan()
    {
        _timeAlive += Time.deltaTime;

        if (_timeAlive >= _aliveTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void ClearTrailRenderer()
    {
        if (_trailRend != null)
            _trailRend.Clear();
    }

    private void OnDisable()
    {
        _timeAlive = 0.0f;
        ClearTrailRenderer();
        _speed = Mathf.Abs(_speed);
        gameObject.SetActive(false);
    }
}
