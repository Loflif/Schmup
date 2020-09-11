using System;
using System.Collections.Generic;
using UnityEngine;

namespace Schmup
{
    public class SphereShipController : MonoBehaviour, IShip
    {
        [Header("Movement")] 
        [SerializeField] private float MovementForce = 1000.0f;

        public float ShieldJuice
        {
            get
            {
                return shieldJuice;
            }
            set
            {
                value = Mathf.Clamp01(value);
                GameManager.Instance.SetShieldJuice(value);
                shieldJuice = value;
            }
        }

        private float shieldJuice;

        [Header("Shield")] 
        [Tooltip("Fraction of shield drain per second")]
        [SerializeField] private float ShieldDrain = 0.5f;
        [Tooltip("Fraction of shield regained per second while not using")]
        [SerializeField] private float ShieldRecovery = 0.5f;

        private List<IWeapon> Weapons = new List<IWeapon>();
        private int CurrentWeaponIterator = 0;
        private IWeapon CurrentWeapon;

        private Vector2 LastMovementInput = Vector2.zero;
        private Rigidbody2D Rigidbody = null;
        private Transform Shield = null;        
        private Transform OwnTransform = null;

        private void Awake()
        {
            ShieldJuice = 1.0f;
            
            OwnTransform = transform;
            Shield = OwnTransform.Find("Shield");
            ToggleShield(false);
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
            Shield.right = aimDirection;
        }

        public void SetAttackInput(bool pIsAttackWanted)
        {
            Weapons[CurrentWeaponIterator].SetAttackInput(pIsAttackWanted);
        }

        public void ToggleShield(bool pActivate)
        {
            Shield.gameObject.SetActive(pActivate);
        }

        private void FixedUpdate()
        {
            Move();
            UpdateShieldValues();
        }

        private void Move()
        {
            Rigidbody.AddForce((MovementForce * Time.fixedDeltaTime) * LastMovementInput);
        }

        private void UpdateShieldValues()
        {
            if (ShieldJuice >= 1.0f
                || ShieldJuice <= 0)
                return;
            if (Shield.gameObject.activeInHierarchy)
            {
                ShieldJuice -= ShieldDrain * Time.fixedDeltaTime;
            }
            else
            {
                ShieldJuice += ShieldRecovery * Time.fixedDeltaTime;
            }
                
            
        }
    }   
}
