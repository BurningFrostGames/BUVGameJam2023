using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace BurningFrost
{
    public class GhostProjectileWeapon : GhostWeapon
    {
        [SerializeField] private float fireRate = 10f;

        [SerializeField] private Projectile projectile;
        [SerializeField] private float projectileSpeed = 10f;

        [SerializeField] private float ammoCost = 10;

        private float _nextTimeToFire;
        
        public override void UseWeapon()
        {
            if (!(Time.time >= _nextTimeToFire)) return;

            SpawnProjectile();
            health.Damage(ammoCost);

            _nextTimeToFire = Time.time + 1f / fireRate;
            
            MMEventManager.TriggerEvent(new GhostParameter
            {
                isNull = false,
                MaxFloat = MaxAmmo,
                CurrentFloat = CurrentAmmo,
                Color = Color
            });
        }

        private void SpawnProjectile()
        {
            var firePoint = weaponHandler.firePoint;
            var bullet = Instantiate(projectile, firePoint.position, firePoint.rotation);
            bullet.AddForce(firePoint.up * projectileSpeed);
            Destroy(bullet, 10f);
        }
    }
}
