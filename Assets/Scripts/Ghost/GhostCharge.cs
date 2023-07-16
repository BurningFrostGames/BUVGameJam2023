using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace BurningFrost
{
    public class GhostCharge : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float force;
        [SerializeField] private float chargeTime;
        [SerializeField] private float moveTime;
        [SerializeField] private float damage;

        private Transform _target;
        private Vector3 _direction;
        [MMReadOnly, SerializeField] private float _currentTime;
        [MMReadOnly, SerializeField] private float _currentMoveTime;

        private bool _isAttacking;
        private Health _health => GetComponentInParent<Health>();

        // Start is called before the first frame update
        void OnEnable()
        {
            _target = GameObject.FindWithTag("Player").transform;
            StartCharge();
        }

        void StartCharge()
        {
            rb.velocity = Vector2.zero;
            _isAttacking = false;

            _currentTime = chargeTime;
            _currentMoveTime = moveTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentTime > 0)
            {
                var dir = _target.position - transform.position;
                _direction = dir;

                _currentTime -= Time.deltaTime;
                return;
            }

            if (!_isAttacking)
            {
                rb.AddForce(_direction * force, ForceMode2D.Impulse);
                _isAttacking = true;
            }

            if (_currentMoveTime > 0)
            {
                _currentMoveTime -= Time.deltaTime;
                return;
            }

            StartCharge();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isAttacking) return;
            
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(damage);
                }

                _health.Die();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _direction);
        }
    }
}
