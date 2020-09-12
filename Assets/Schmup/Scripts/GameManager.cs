﻿using UnityEngine;
using UnityEngine.UI;

namespace Schmup
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        private RectTransform ShieldFillRect = null;
        private Image ShieldFillImage = null;

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
            
            GameObject shieldFill = GameObject.Find("ShieldFill");
            ShieldFillRect = shieldFill.GetComponent<RectTransform>();
            ShieldFillImage = shieldFill.GetComponent<Image>();
        }

        public void SetShieldJuice(float pValue)
        {
            Vector3 shieldFillScale = ShieldFillRect.localScale;
            shieldFillScale.x = pValue;
            ShieldFillRect.localScale = shieldFillScale;
        }

        public void SetShieldMeterColor(Color pColor)
        {
            ShieldFillImage.color = pColor;
        }
        
    }   
}
