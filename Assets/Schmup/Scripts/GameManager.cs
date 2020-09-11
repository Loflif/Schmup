using UnityEngine;

namespace Schmup
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        private RectTransform ShieldFill = null;

        public static GameManager Instance
        {
            get;
            private set;
        }
        

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
            ShieldFill = GameObject.Find("ShieldFill").GetComponent<RectTransform>();
        }

        public void SetShieldJuice(float pValue)
        {
            Vector3 shieldFillScale = ShieldFill.localScale;
            shieldFillScale.x = pValue;
            ShieldFill.localScale = shieldFillScale;
        }
        
    }   
}
