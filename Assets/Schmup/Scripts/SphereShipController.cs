using System;
using System.Collections.Generic;
using UnityEngine;

namespace Schmup
{
    public class SphereShipController : MonoBehaviour, IShip
    {
        [Header("Movement")] 
        [SerializeField] private float MovementForce = 1000.0f;

        private List<IWeapon> Weapons = new List<IWeapon>();
        private int CurrentWeaponIterator = 0;
        private IWeapon CurrentWeapon;

        private Vector2 LastMovementInput = Vector2.zero;
        private Rigidbody2D Rigidbody = null;
        private ShieldController Shield = null;        
        private Transform OwnTransform = null;

        private void Awake()
        {
            OwnTransform = transform;
            Shield = GetComponentInChildren<ShieldController>();
            Rigidbody = GetComponent<Rigidbody2D>();
            IWeapon startWeapon = GetComponentInChildren<IWeapon>();
            Weapons.Add(startWeapon);
            CurrentWeaponIterator = Weapons.IndexOf(startWeapon);
        }

        private void Start()
        {
            Weapons[CurrentWeaponIterator].Attach(OwnTransform);
        }

        public void UpdateMovementVector(Vector2 pMovementDirection)
        {
            LastMovementInput = pMovementDirection;
        }
        
        public void UpdateAimVector(Vector2 pWorldSpaceMousePosition)
        {
            Vector2 aimDirection = pWorldSpaceMousePosition - (Vector2) OwnTransform.position;
            aimDirection.Normalize();
            Shield.Aim(aimDirection);
        }

        public void SetAttackInput(bool pIsAttackWanted)
        {
            Weapons[CurrentWeaponIterator].SetAttackInput(pIsAttackWanted);
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void ToggleShield(bool pActivate)
        {
            Shield.Toggle(pActivate);
        }

        private void Move()
        {
            Rigidbody.AddForce((MovementForce * Time.fixedDeltaTime) * LastMovementInput);
        }
    }   
}
