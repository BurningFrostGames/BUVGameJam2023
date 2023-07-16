using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningFrost
{
    public class GhostBasicMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 10f;

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
            var dir = _target.position - transform.position;
            dir.Normalize();
            _movement = dir;
        }

        private void FixedUpdate()
        {
            Move(_movement);
        }

        void Move(Vector2 dir)
        {
            rb.MovePosition((Vector2)transform.position + dir * (speed * Time.deltaTime));
        }
    }
}
