using UnityEngine;

namespace Schmup
{
    public class DisableBoundary3D : MonoBehaviour
    {
        private void OnTriggerExit(Collider pOther)
        {
            pOther.gameObject.SetActive(false);
        }
    }   
}
