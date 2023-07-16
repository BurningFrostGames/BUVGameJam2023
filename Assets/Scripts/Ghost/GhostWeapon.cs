using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.Serialization;

public class GhostWeapon : MonoBehaviour
{
    private GhostHealth health => GetComponent<GhostHealth>();
    public float MaxAmmo => health.maxHealth;
    public float CurrentAmmo => health.currentHealth;

    public Projectile Projectile;
    public float HealthPerAmmoFactor = 10;
    
    public Color Color;

    public void OnInjected()
    {
        health.useEvent = false;
    }

    public void OnEjected()
    {
        health.useEvent = true;
    }
    
    public virtual void ConsumeAmmo()
    {
        health.Damage(HealthPerAmmoFactor);
    }
    
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    
    public void AddForce(Vector3 direction)
    {
        rb.AddForce(direction, ForceMode2D.Impulse);
    }
}
