using Unity.Mathematics;
using UnityEngine;

namespace Schmup
{
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField] private GameObject ExplodingParticle = null; //TODO: Objectpool
        
        private void OnCollisionEnter2D(Collision2D pOther)
        {
            if (pOther.transform.CompareTag("Player"))
            {
                Die();
            }

        }

        private void Die()
        {
            Instantiate(ExplodingParticle, transform.position, quaternion.identity);
            Destroy(gameObject); //TODO: return to objectpool
        }

        private void OnTriggerEnter2D(Collider2D pOther)
        {
            if (pOther.transform.CompareTag("Player"))
            {
                Die();
            }
        }

        private void OnParticleCollision(GameObject other)
        {
            Die();
        }
    }   
}
