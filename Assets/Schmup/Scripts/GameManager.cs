using UnityEngine;

namespace Schmup
{
    public class GameManager : MonoBehaviour
    {
        private RectTransform ShieldFill = null;

        public static GameManager Instance
        {
            get
            {
                if (Instance != null) 
                    return Instance;
                
                Instance = GameObject.FindObjectOfType<GameManager>();

                if (Instance != null) 
                    return Instance;
                
                GameObject container = GameObject.Find("Managers");
                if (container == null)
                {
                    container = new GameObject("Managers");
                }
                Instance = container.AddComponent<GameManager>();
                return Instance;
            }
            private set{}
        }
        

        private void Awake()
        {
            Instance = GetComponent<GameManager>();
            ShieldFill = GameObject.Find("ShieldFill").GetComponent<RectTransform>();
        }

        public void SetShieldJuice(float pValue)
        {
            Vector3 shieldFillScale = ShieldFill.localScale;
            shieldFillScale.x += pValue;
            ShieldFill.localScale = shieldFillScale;
        }
        
    }   
}
