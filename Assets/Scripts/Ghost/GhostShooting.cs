using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningFrost
{
    public class GhostShooting : MonoBehaviour
    {
        [SerializeField] private Projectile projectile;
        [SerializeField] private Transform rotationRoot;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float attackRange = 12f;
        [SerializeField] private float fireRate;

        private Transform _target;
        private float _nextTimeToFire;
        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            Vector3 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rotationRoot.rotation = Quaternion.AngleAxis(angle, rotationRoot.forward);

            if (Vector3.Distance(_target.position, transform.position) <= attackRange)
            {
                if (!(Time.time >= _nextTimeToFire)) return;

                SpawnProjectile(firePoint);

                _nextTimeToFire = Time.time + 1f / fireRate;
            }
        }
        
        public void SpawnProjectile(Transform point)
        {
            var bullet = Instantiate(projectile, point.position, point.rotation);
            bullet.AddForce(point.up);
            Destroy(bullet, 10f);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
