using System.Collections;
using System.Collections.Generic;
using BurningFrost;
using MoreMountains.Tools;
using UnityEngine;

public class GhostChargeWeapon : GhostWeapon
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float spawnOffset;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float holdTime = 5f;

    private float _currentTime;
    private bool _fired;
    
    public override void PressedWeapon()
    {
        _currentTime = holdTime;
        MMEventManager.TriggerEvent(new GhostParameter
        {
            isNull = false,
            MaxFloat = holdTime,
            CurrentFloat = _currentTime,
            Color = Color
        });
    }

    public override void UseWeapon()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            MMEventManager.TriggerEvent(new GhostParameter
            {
                isNull = false,
                MaxFloat = holdTime,
                CurrentFloat = _currentTime,
                Color = Color
            });
            return;
        }

        if (!_fired)
        {
            SpawnProjectile();
            _fired = true;
            health.Die();
        }
    }

    public override void ReleasedWeapon()
    {
        MMEventManager.TriggerEvent(new GhostParameter
        {
            isNull = false,
            MaxFloat = holdTime,
            CurrentFloat = holdTime,
            Color = Color
        });
    }

    private void SpawnProjectile()
    {
        var firePoint = weaponHandler.firePoint;
        var bullet = Instantiate(projectile, firePoint.position + firePoint.up * spawnOffset, firePoint.rotation);
        bullet.AddForce(firePoint.up * projectileSpeed);
        Destroy(bullet, 10f);
    }
}
