using UnityEngine;

namespace Schmup
{
    interface IShip
    {
        void UpdateMovementVector(Vector2 pMovementDirection);
        void UpdateAimVector(Vector2 pAimDirection);
        void SetAttackInput(bool pIsAttackWanted);
    }   
}
