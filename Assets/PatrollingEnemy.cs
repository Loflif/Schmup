using UnityEngine;

namespace Schmup
{
    public class PatrollingEnemy : MonoBehaviour
    {
        [SerializeField] private Vector2 StartVelocity = new Vector2(-4, 0);

        private bool IsActivated = false;
        private ShootHomingMissles ShootingScript = null;
        private Rigidbody2D Rigidbody;
        private void Awake()
        {
            ShootingScript = GetComponent<ShootHomingMissles>();
            Rigidbody = GetComponent<Rigidbody2D>();
            ShootingScript.enabled = false;
        }

        private void Start()
        {
            Rigidbody.velocity = StartVelocity;
        }

        private void FixedUpdate()
        {
            if (!IsActivated)
                return;
            
            Rigidbody.velocity = Vector3.up * Mathf.Cos(Time.time);
        }

        private void OnTriggerEnter2D(Collider2D pOther)
        {
            if (IsActivated)
                return;

            if (pOther.CompareTag("EnemyActivation"))
            {
                IsActivated = true;
                ShootingScript.enabled = true;
                Rigidbody.velocity = Vector2.zero;
            }
        }
    }
}