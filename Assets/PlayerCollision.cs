using System;
using UnityEngine;

namespace Schmup
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private int MaxHealth = 20;
        private int CurrentHealth = 0;

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        private void OnParticleCollision(GameObject pOther)
        {
            TakeDamage();
        }

        private void TakeDamage()
        {
            CurrentHealth--;
            Debug.Log(CurrentHealth);
        }
    }
}
