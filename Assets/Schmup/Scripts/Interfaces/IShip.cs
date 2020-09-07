using UnityEngine;

namespace Schmup
{
    public abstract class IShip : MonoBehaviour
    {
        public abstract void UpdateMovementVector(Vector2 pMovementInput);

    }   
}
