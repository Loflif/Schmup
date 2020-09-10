using UnityEngine;

namespace Schmup
{
    public class PlayerInput : MonoBehaviour
    {
        private InputMaster Input = null;
        private IShip CurrentShip = null;

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
            
            Input.Player.Attack.performed -= context => CurrentShip.SetAttackInput(true);
            Input.Player.Attack.canceled -= context => CurrentShip.SetAttackInput(false);
        }

        private void BindNewInput()
        {
            Input.Player.Move.performed += context => CurrentShip.UpdateMovementVector(context.ReadValue<Vector2>());
            
            Input.Player.Attack.performed += context => CurrentShip.SetAttackInput(true);
            Input.Player.Attack.canceled += context => CurrentShip.SetAttackInput(false);
        }
    }   
}