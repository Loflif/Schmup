using UnityEngine;

namespace Schmup
{
    public interface IWeapon
    {
        void SetAttackInput(bool pIsAttackWanted);
        void Attach(Transform pParent);
    }    
}

