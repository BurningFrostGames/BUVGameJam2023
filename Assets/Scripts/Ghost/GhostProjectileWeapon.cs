using System.Collections;
using System.Collections.Generic;
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

        protected override void Attack()
        {
            base.Attack();

            if (!(Time.time >= _nextTimeToFire)) return;

            SpawnProjectile();
            health.Damage(ammoCost);

            _nextTimeToFire = Time.time + 1f / fireRate;
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
