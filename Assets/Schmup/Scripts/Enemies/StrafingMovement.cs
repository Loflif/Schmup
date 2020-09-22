using UnityEngine;

namespace Schmup
{
    public class StrafingMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 StartVelocity = Vector2.zero;
        [SerializeField] private Vector2 ConstantVelocityChange = Vector2.zero;

        private bool IsActivated = false;

        private Rigidbody2D Rigidbody = null;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Rigidbody.velocity = StartVelocity;
        }

        private void FixedUpdate()
        {
            if (!IsActivated) 
                return;
            
            Vector2 newVelocity = Rigidbody.velocity;
            newVelocity += ConstantVelocityChange * Time.fixedDeltaTime;

            Rigidbody.velocity = newVelocity;
        }

        private void OnTriggerEnter2D(Collider2D pOther)
        {
            if (IsActivated)
                return;
            
            if (pOther.CompareTag("EnemyActivation"))
            {
                IsActivated = true;
            }
        }
    }   
}
