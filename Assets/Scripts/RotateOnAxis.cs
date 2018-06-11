using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    [SerializeField]
    private bool _z;
    [SerializeField]
    private float _rotationSpeed;

    private void Update()
    {
        if (_z)
        {
            transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
        }
    }
}
