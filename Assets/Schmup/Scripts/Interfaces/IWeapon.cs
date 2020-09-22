using UnityEngine;

namespace Schmup
{
    public interface IWeapon
    {
        void SetAttackInput(bool pIsAttackWanted);
        void Attach(Transform pParent);
        void Aim(Vector2 pAimDirection);
        void Toggle(bool pActive);
    }    
}

