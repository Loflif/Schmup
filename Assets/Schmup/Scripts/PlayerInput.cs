using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Schmup
{
    public class PlayerInput : MonoBehaviour
    {
        private InputMaster Input = null;
        private IShip CurrentShip = null;

        private Vector2 MovementInput = Vector2.zero;

        private void Awake()
        {
            Input = new InputMaster();
            Input.Enable();
            CurrentShip = GetComponent<IShip>();
            BindNewInput();
        }

        private void SetShip(IShip pNewShip)
        {
            UnbindInput();
            CurrentShip = pNewShip;
            BindNewInput();
        }
        
        private void UnbindInput()
        {
            Input.Player.Move.performed -= context => CurrentShip.UpdateMovementVector(context.ReadValue<Vector2>());
            Input.Player.Move.canceled -= context => CurrentShip.UpdateMovementVector(Vector2.zero);
        }

        private void BindNewInput()
        {
            Input.Player.Move.performed += context => CurrentShip.UpdateMovementVector(context.ReadValue<Vector2>());
            Input.Player.Move.canceled += context => CurrentShip.UpdateMovementVector(Vector2.zero);
        }
    }   
}