using System;
using UnityEngine;

namespace Schmup
{
    public class SphereShipController : MonoBehaviour, IShip
    {
        [Header("Movement")] 
        [SerializeField] private float MovementForce = 1000.0f;
        
        private Rigidbody2D Rigidbody = null;

        private Vector2 LastMovementInput = Vector2.zero;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        public void UpdateMovementVector(Vector2 pMovementDirection)
        {
            LastMovementInput = pMovementDirection;
        }
        
        public void UpdateAimVector(Vector2 pAimDirection)
        {
            
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            Rigidbody.AddForce((MovementForce * Time.fixedDeltaTime) * LastMovementInput);
        }

        
    }   
}
