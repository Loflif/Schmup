﻿using UnityEngine;
using UnityEngine.UI;

namespace Schmup
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : MonoBehaviour
    {
        private RectTransform ShieldFillRect = null;
        private RectTransform HealthFillRect = null;
        private Image ShieldFillImage = null;
        private GameObject PauseScreen = null;

        private bool IsPaused = false;

        public Transform PlayerTransform = null;
        
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
            
            HealthFillRect = GameObject.Find("HealthFill").GetComponent<RectTransform>();

            PauseScreen = GameObject.Find("PauseScreen");

            PlayerTransform = GameObject.Find("PlayerShip").transform;
        }

        private void Start()
        {
            TogglePause();
        }

        public void SetShieldJuice(float pValue)
        {
            Vector3 shieldFillScale = ShieldFillRect.localScale;
            shieldFillScale.x = pValue;
            ShieldFillRect.localScale = shieldFillScale;
        }

        public void SetHealthMeter(float pValue)
        {
            Vector3 healthFillScale = HealthFillRect.localScale;
            healthFillScale.x = pValue;
            HealthFillRect.localScale = healthFillScale;
        }

        public void SetShieldMeterColor(Color pColor)
        {
            ShieldFillImage.color = pColor;
        }

        public void TogglePause()
        {
            if (!IsPaused)
            {
                PauseScreen.SetActive(true);
                IsPaused = true;
                Time.timeScale = 0;
            }
            else
            {
                PauseScreen.SetActive(false);
                IsPaused = false;
                Time.timeScale = 1;
            }
        }
    }   
}
