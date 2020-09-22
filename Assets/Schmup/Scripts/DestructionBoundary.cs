using UnityEngine;

namespace Schmup
{
    public class DestructionBoundary : MonoBehaviour
    {
        private void OnTriggerExit(Collider pOther)
        {
            Destroy(pOther.gameObject);
        }
    }   
}
