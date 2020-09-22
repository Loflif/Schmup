using UnityEngine;

namespace Schmup
{
    public class HomingMissile : MonoBehaviour
    {
        [SerializeField] private float Steering = 30.0f;
        [SerializeField] private float StartForce = 100.0f;
        [SerializeField] private float ForwardsForce = 30.0f;
        [SerializeField] private GameObject MissilExplosion = null;

        private Rigidbody2D Rigidbody = null;
        private Transform OwnTransform = null;
        
        private void Awake()
        {
            OwnTransform = transform;
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Rigidbody.AddForce(OwnTransform.forward * StartForce);
        }

        private void FixedUpdate()
        {
            RotateTowardsPlayer();
            MoveForwards();
        }

        private void RotateTowardsPlayer()
        {
            Vector3 directionTowardsPlayer = (GameManager.Instance.PlayerTransform.position - OwnTransform.position).normalized;
            Quaternion facingPlayer = Quaternion.LookRotation(directionTowardsPlayer);
            OwnTransform.rotation =
                Quaternion.RotateTowards(transform.rotation, facingPlayer, Steering * Time.fixedDeltaTime);
        }

        private void MoveForwards()
        {
            Rigidbody.AddForce(OwnTransform.forward * (ForwardsForce * Time.fixedDeltaTime)); 
        }

        private void OnCollisionEnter2D(Collision2D pOther)
        {
            if (pOther.transform.CompareTag("Player"))
            {
                Instantiate(MissilExplosion, OwnTransform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }   
}
