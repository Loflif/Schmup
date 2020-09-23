using System.Collections.Generic;
using UnityEngine;

namespace Schmup
{
    public class PlayerController : MonoBehaviour, IShip
    {
        [Header("Movement")] 
        [SerializeField] private float MovementForce = 1000.0f;

        private List<IWeapon> Weapons = new List<IWeapon>();

        private int currentWeaponIterator = 0;
        private int CurrentWeaponIterator
        {
            get { return currentWeaponIterator; }
            set
            {
                if (value >= Weapons.Count)
                {
                    currentWeaponIterator = 0;
                }
                else if (value < 0)
                {
                    currentWeaponIterator = Weapons.Count - 1;
                }
                else
                {
                    currentWeaponIterator = value;
                }
            }
        }

        private IWeapon CurrentWeapon;

        private Vector2 LastMovementInput = Vector2.zero;
        private Rigidbody2D Rigidbody = null;
        private ShieldController Shield = null;        
        private Transform OwnTransform = null;
        private Transform WeaponParent = null;
        private MeshRenderer ShipMesh = null;
        private Collider2D ShipCollider = null;
        
        
        private void Awake()
        {
            OwnTransform = transform;
            WeaponParent = OwnTransform.Find("Weapons");
            Shield = GetComponentInChildren<ShieldController>();
            Rigidbody = GetComponent<Rigidbody2D>();
            
            IWeapon startWeapon = GetComponentInChildren<IWeapon>();
            Weapons.Add(startWeapon);
            CurrentWeaponIterator = Weapons.IndexOf(startWeapon);
            CurrentWeapon = startWeapon;
            
            ShipMesh = GetComponent<MeshRenderer>();
            ShipCollider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            CurrentWeapon.Attach(WeaponParent);
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
            CurrentWeapon.Aim(aimDirection);
        }

        public void SetAttackInput(bool pIsAttackWanted)
        {
            CurrentWeapon.SetAttackInput(pIsAttackWanted);
        }

        public void NextWeapon()
        {
            CurrentWeapon.Toggle(false);
            CurrentWeaponIterator++;
            CurrentWeapon = Weapons[CurrentWeaponIterator];
            CurrentWeapon.Toggle(true);
        }

        public void PreviousWeapon()
        {
            CurrentWeapon.Toggle(false);
            CurrentWeaponIterator--;
            CurrentWeapon = Weapons[CurrentWeaponIterator];
            CurrentWeapon.Toggle(true);
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void ToggleShield(bool pActivate)
        {
            Shield.Toggle(pActivate);
        }

        public void PickupWeapon(IWeapon pPickup)
        {
            pPickup.Attach(WeaponParent);
            Weapons.Add(pPickup);
            NextWeapon();
        }

        private void Move()
        {
            Rigidbody.AddForce((MovementForce * Time.fixedDeltaTime) * LastMovementInput);
        }

        public void DisableAssets()
        {
            Shield.gameObject.SetActive(false);
            CurrentWeapon.Toggle(false);
            ShipCollider.enabled = false;
            ShipMesh.enabled = false;
            
            GameManager.Instance.DelayedLoseScreenActivation();
        }
    }   
}
