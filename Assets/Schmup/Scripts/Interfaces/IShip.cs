using System.Collections.Generic;
using UnityEngine;

namespace Schmup
{
    interface IShip
    {
        float ShieldJuice { get; set; }
        
        void UpdateMovementVector(Vector2 pMovementDirection);
        void UpdateAimVector(Vector2 pWorldSpaceMousePosition);
        void SetAttackInput(bool pIsAttackWanted);
        void ToggleShield(bool pActivate);
    }  
}
