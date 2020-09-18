using UnityEngine;

namespace Schmup
{
    public class PlayerCollision : MonoBehaviour
    {
        [SerializeField] private float MaxHealth = 20.0f;
        private float CurrentHealth = 0.0f;
        

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
