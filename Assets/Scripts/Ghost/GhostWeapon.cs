using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;
using UnityEngine.Serialization;

namespace BurningFrost
{
    public abstract class GhostWeapon : MonoBehaviour
    {
        public float MaxAmmo => health.maxHealth;
        public float CurrentAmmo => health.currentHealth;

        public GameObject AIBody;
        public Color Color = Color.white;
        public float EjectStunTime = 2f;

        protected GhostWeaponHandler weaponHandler;
        protected Health health => GetComponent<Health>();

        public void OnInjected(GhostWeaponHandler handler)
        {
            health.useEvent = false;
            weaponHandler = handler;

            AIBody.SetActive(false);
        }

        public void OnEjected()
        {
            health.useEvent = true;
            weaponHandler = null;

            StartCoroutine(Stun());
        }

        IEnumerator Stun()
        {
            yield return new WaitForSeconds(EjectStunTime);
            PostStun();
        }

        public virtual void PostStun()
        {
            AIBody.SetActive(true);
        }

        public void UseWeapon()
        {
            Attack();

            MMEventManager.TriggerEvent(new GhostParameter
            {
                Ghost = this
            });
        }

        protected virtual void Attack()
        {

        }

        private Rigidbody2D rb => GetComponent<Rigidbody2D>();

        public void AddForce(Vector3 direction)
        {
            rb.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}
