using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

            MainCamera = Camera.main;
            CameraOffset = transform.position.z - MainCamera.transform.position.z;
        }

        private void BindInput()
        {
            Input.Player.Move.performed += SendMovementVector;
            
            Input.Player.Attack.performed += SendAttackInput;
            Input.Player.Attack.canceled += SendAttackInput;
            
            Input.Player.Look.performed += SendMousePosition;

            Input.Player.Shield.performed += SendShieldInput;
            Input.Player.Shield.canceled += SendShieldInput;

            Input.Player.SwitchWeapon.performed += SwitchWeapon;

            Input.Player.Pause.performed += SendPauseInput;

            Input.Player.Reset.canceled += SendResetInput;
        }

        private void UnbindInput()
        {
            Input.Player.Move.performed -= SendMovementVector;
            
            Input.Player.Attack.performed -= SendAttackInput;
            Input.Player.Attack.canceled -= SendAttackInput;
            
            Input.Player.Look.performed -= SendMousePosition;

            Input.Player.Shield.performed -= SendShieldInput;
            Input.Player.Shield.canceled -= SendShieldInput;

            Input.Player.SwitchWeapon.performed -= SwitchWeapon;

            Input.Player.Pause.performed -= SendPauseInput;

            Input.Player.Reset.canceled -= SendResetInput;
        }

        private void SendMovementVector(InputAction.CallbackContext pContext)
        {
            CurrentShip.UpdateMovementVector(pContext.ReadValue<Vector2>());
        }

        private void SendAttackInput(InputAction.CallbackContext pContext)
        {
            CurrentShip.SetAttackInput(pContext.performed);
        }

        private void SwitchWeapon(InputAction.CallbackContext pContext)
        {
            float scrollValue = pContext.ReadValue<float>();
            if (scrollValue > 0)
                CurrentShip.NextWeapon();
            else
                CurrentShip.PreviousWeapon();
        }

        private void SendMousePosition(InputAction.CallbackContext pContext)
        {
            Vector2 mousePosition = pContext.ReadValue<Vector2>();
            
            Vector3 mousePositionWorld = new Vector3(mousePosition.x, mousePosition.y, CameraOffset);
            CurrentShip.UpdateAimVector(MainCamera.ScreenToWorldPoint(mousePositionWorld));
        }

        private void SendShieldInput(InputAction.CallbackContext pContext)
        {
            CurrentShip.ToggleShield(pContext.performed);
        }

        private void SendPauseInput(InputAction.CallbackContext pContext)
        {
            GameManager.Instance.TogglePause();
        }

        private void SendResetInput(InputAction.CallbackContext pContext)
        {
            GameManager.Instance.ResetGame();
        }

        private void OnEnable()
        {
            BindInput();
        }

        private void OnDisable()
        {
            UnbindInput();
        }
    }   
}