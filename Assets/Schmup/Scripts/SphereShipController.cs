using System;
using UnityEngine;

namespace Schmup
{
    public class SphereShipController : IShip
    {
        [Header("Movement")] 
        [SerializeField] private float MovementForce = 1000.0f;
        
        private Rigidbody Rigidbody = null;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }


        public override void UpdateMovementVector(Vector2 pMovementInput)
        {
            // Rigidbody.AddForce()
        }

        private void Update()
        {
            
        }
    }   
}
