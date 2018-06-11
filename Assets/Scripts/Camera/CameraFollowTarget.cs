using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;
    [SerializeField]
    private float _followSpeed = 5.0f;

    private Vector2 _movement = Vector2.zero;

    private void Awake()
    {
        SpawnPlayer.OnPlayerSpawn += RegisterTargetTransform;
    }

    private void Update()
    {
        if (_targetTransform != null)
        {
            Vector2 direction = (_targetTransform.position - transform.position).normalized;
            _movement = direction * _followSpeed;
        }
        else
        {
            _movement = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        if (_movement != Vector2.zero)
        {
            Vector3 newMovement = new Vector3(_movement.x, _movement.y, 0);

            transform.position += newMovement * Time.deltaTime;
        }
    }

    private void RegisterTargetTransform(GameObject targetObject)
    {
        _targetTransform = targetObject.transform;
    }

    private void OnDestroy()
    {
        SpawnPlayer.OnPlayerSpawn -= RegisterTargetTransform;
    }
}
