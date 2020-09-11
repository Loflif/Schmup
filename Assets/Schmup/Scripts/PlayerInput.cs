using UnityEngine;

namespace Schmup
{
    public class PlayerInput : MonoBehaviour
    {
        private InputMaster Input = null;
        private IShip CurrentShip = null;

        private Camera MainCamera = null;

        private float CameraOffset = 0.0f;
        private void Awake()
        {
            Input = new InputMaster();
            Input.Enable();
            
            CurrentShip = GetComponent<IShip>();
            BindInput();

            MainCamera = Camera.main;
            CameraOffset = transform.position.z - MainCamera.transform.position.z;
        }

        private void SetShip(IShip pNewShip)
        {
            CurrentShip = pNewShip;
        }

        private void BindInput()
        {
            Input.Player.Move.performed += context => SendMovementVector(context.ReadValue<Vector2>());
            
            Input.Player.Attack.performed += context => SendAttackInput(true);
            Input.Player.Attack.canceled += context => SendAttackInput(false);
            
            Input.Player.Look.performed += context => SendMousePosition(context.ReadValue<Vector2>());

            Input.Player.Shield.performed += context => SendShieldInput(true);
            Input.Player.Shield.canceled += context => SendShieldInput(false);
        }

        private void SendMovementVector(Vector2 pMovementInput)
        {
            CurrentShip.UpdateMovementVector(pMovementInput);
        }

        private void SendAttackInput(bool pIsAttackWanted)
        {
            CurrentShip.SetAttackInput(pIsAttackWanted);
        }

        private void SendMousePosition(Vector2 pMousePosition)
        {
            Vector3 mousePositionWorld = new Vector3(pMousePosition.x, pMousePosition.y, CameraOffset);
            CurrentShip.UpdateAimVector(MainCamera.ScreenToWorldPoint(mousePositionWorld));
        }

        private void SendShieldInput(bool pEnable)
        {
            CurrentShip.ToggleShield(pEnable);
        }
    }   
}