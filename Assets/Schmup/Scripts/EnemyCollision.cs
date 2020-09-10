using UnityEngine;

namespace Schmup
{
    public class EnemyCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D pOther)
        {
            if (pOther.transform.CompareTag("Player"))
            {
                Die();
            }

        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }   
}
