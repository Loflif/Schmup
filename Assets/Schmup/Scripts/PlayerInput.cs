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
            CurrentShip = GetComponent<IShip>();

            Input.Player.Move.performed += context => CurrentShip.UpdateMovementVector(context.ReadValue<Vector2>());
        }

        private void SetShip(IShip pNewShip)
        {
            Input.Player.Move.performed -= context => CurrentShip.UpdateMovementVector(context.ReadValue<Vector2>());
            
            
            CurrentShip = pNewShip;
        }

        private void Update()
        {
            
            
            Vector2 movementInput = Input.Player.Move.ReadValue<Vector2>();
            
        }
    }   
}