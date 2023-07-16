 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public LayerMask damageMask;

    public float despawnTime = 10f;

    private void Start()
    {
        Destroy(gameObject, despawnTime);
    }

    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    
    public void AddForce(Vector3 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (damageMask.Contains(other.gameObject.layer) && other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Damage(damage);
        }

        Debug.Log($"Collided with {other.name}");
        Destroy(gameObject);
    }
}
