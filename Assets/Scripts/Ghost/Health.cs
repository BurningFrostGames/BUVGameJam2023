using System.Collections;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;

namespace BurningFrost
{
    public class Health : MonoBehaviour, IDamageable
    {
        public float maxHealth = 100f;
        [MMReadOnly] public float currentHealth;

        [Space] public bool useEvent = true;

        public UnityEvent<float> OnHealthChange;
        public UnityEvent<float> OnDamaged;
        public UnityEvent OnDeath;

        private bool _isDead;

        // Start is called before the first frame update
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void Damage(float damage)
        {
            if (_isDead) return;

            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Die();
                return;
            }

            if (useEvent)
            {
                OnHealthChange.Invoke(currentHealth);
                OnDamaged.Invoke(damage);
            }
        }

        public void Die()
        {
            currentHealth = 0;
            _isDead = true;

            if (useEvent)
                OnDeath.Invoke();
        }

        public void DestroyOnDeath(float delay)
        {
            Destroy(gameObject, delay);
        }
    }
}