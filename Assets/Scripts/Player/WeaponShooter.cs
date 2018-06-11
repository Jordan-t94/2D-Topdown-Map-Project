using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    [SerializeField]
    private Transform _bulletSpawnPoint;
    [SerializeField]
    private float _rotationOffset;
    [SerializeField]
    private float _fireRate = 0.25f;

    [Header("Bullet Pooler")]
    [SerializeField]
    private ObjectPooler _bulletPool;

    private float _shootTimer = 0.0f;
    private bool _shooterEnabled = true;

    private void Awake()
    {
        _shootTimer = _fireRate;
        _bulletPool.PoolObjects(GameController.Instance.BulletSpawnParent);
    }

    private void Update()
    {
        UpdateShootTimer();

        if (Input.GetButton("Fire1"))
            FireWeapon();
    }

    private void UpdateShootTimer()
    {
        if (_shootTimer < _fireRate)
        {
            _shootTimer += Time.deltaTime;
        }
    }

    private void FireWeapon()
    {
        if (_shootTimer >= _fireRate && _shooterEnabled)
        {
            SpawnBullet();

            _shootTimer = 0.0f;
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = _bulletPool.GetPooledObject();
        SetBulletPositionAndRotation(bullet);
        bullet.SetActive(true);
    }

    private void SetBulletPositionAndRotation(GameObject bullet)
    {
        Vector3 vectorToTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _bulletSpawnPoint.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + _rotationOffset;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        bullet.transform.position = _bulletSpawnPoint.position;
        bullet.transform.rotation = rotation;
    }

    private void OnDestroy()
    {
        _bulletPool.ClearPool();
    }
}
