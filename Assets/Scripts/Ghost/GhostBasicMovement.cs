using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BurningFrost
{
    public class GhostBasicMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float attackRange = 1f;

        private Transform _target;
        private Vector3 _movement;

        // Start is called before the first frame update
        void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            var direction = _target.position - transform.position;
            direction.Normalize();
            _movement = direction;
        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(_target.position, transform.position) > attackRange)
            {
                Move(_movement);
            }
        }

        void Move(Vector2 dir)
        {
            rb.MovePosition((Vector2)transform.position + dir * (speed * Time.deltaTime));
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
