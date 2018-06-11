using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollide : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount = 10.0f;

    private void OnCollisionStay2D(Collision2D collision)
    {
        HealthController healthController = collision.gameObject.GetComponent<HealthController>();
        if (healthController != null)
        {
            Debug.Log("Hit Damageable Target");
            healthController.ReduceHealth(_damageAmount);
        }
    }
}
