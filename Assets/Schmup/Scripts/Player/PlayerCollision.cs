﻿using UnityEngine;

namespace Schmup
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private float MaxHealth = 20.0f;
        private float CurrentHealth = 0.0f;
        private PlayerController PlayerController = null;

        private void Awake()
        {
            PlayerController = GetComponentInParent<PlayerController>();
            CurrentHealth = MaxHealth;
        }

        private void OnParticleCollision(GameObject pOther)
        {
            TakeDamage();
        }
        
        private void OnCollisionEnter2D(Collision2D pOther)
        {
            if(pOther.transform.CompareTag("Enemy"))
                TakeDamage();
        }

        private void OnTriggerEnter2D(Collider2D pOther)
        {
            if (pOther.transform.CompareTag("WeaponPickup"))
            {
                IWeapon newWeapon = pOther.GetComponentInChildren<IWeapon>();
                PlayerController.PickupWeapon(newWeapon);
            }
        }

        private void TakeDamage()
        {
            if (CurrentHealth <= 0)
                return;
            
            CurrentHealth--;
            if (CurrentHealth <= 0)
            {
                Die();
            }
            GameManager.Instance.SetHealthMeter(CurrentHealth/MaxHealth);
        }

        private void Die()
        {
            //Explode
            //Disable stuffs
        }
    }
}