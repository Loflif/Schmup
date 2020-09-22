using System;
using UnityEngine;

namespace Schmup
{
    public class ShieldController : MonoBehaviour
    {
        [Tooltip("Fraction of shield regained per second while not using")]
        [SerializeField] private float Recovery = 0.5f;
        [Tooltip("Amount of shield juice required to toggle the shield on 1st ColorKey is set at MinimumToggleThreshold")]
        [SerializeField] private float MinimumToggleThreshold = 0.2f;
        [Tooltip("Color of Meter based on level")]
        [SerializeField] private Gradient MeterLevelColor = null;

        [SerializeField] private float ParticleCollisionDamage = 0.1f;
        [SerializeField] private float EnemyCollisionDamage = 0.1f;
        
        private float juice;

        private bool IsActive = true;

        private Transform OwnTransform = null;
        private Collider2D Collider = null;
        private Transform Child = null;

        public float Juice
        {
            get
            {
                return juice;
            }
            set
            {
                GameManager.Instance.SetShieldJuice(value);
                juice = value;
                UpdateMeterColor();
            }
        }

        private void Awake()
        {
            OwnTransform = transform;
            Collider = GetComponent<Collider2D>();
            Child = OwnTransform.GetChild(0);
            Juice = 1.0f;
            
            GradientColorKey[] colorKey = MeterLevelColor.colorKeys;
            colorKey[0].time = MinimumToggleThreshold;
            MeterLevelColor.SetKeys(colorKey, MeterLevelColor.alphaKeys);
            
            Toggle(false);
        }
        
        public void Toggle(bool pActivate)
        {
            if (pActivate && Juice > MinimumToggleThreshold)
            {
                IsActive = true;
                Collider.enabled = true;
                Child.gameObject.SetActive(true);
            }
            else if (!pActivate)
            {
                IsActive = false;
                Collider.enabled = false;
                Child.gameObject.SetActive(false);
            }
        }

        public void Aim(Vector2 pAimDirection)
        {
             OwnTransform.right = pAimDirection;
        }

        private void Update()
        {
            if (!IsActive)
                RechargeJuice();
        }

        private void RechargeJuice()
        {
            if (Juice >= 1.0f)
            {
                Juice = 1.0f;
                return;
            }
            Juice += Recovery * Time.deltaTime;
        }

        private void OnCollisionEnter2D(Collision2D pOther)
        {
            TakeDamage(EnemyCollisionDamage);
        }

        private void OnParticleCollision(GameObject pOther)
        {
            TakeDamage(ParticleCollisionDamage);
        }

        private void TakeDamage(float pDamageTaken)
        {
            Juice -= pDamageTaken;
            if (Juice <= 0)
                Toggle(false);
        }

        private void UpdateMeterColor()
        {
            Color gradientColor = MeterLevelColor.Evaluate(juice);
            GameManager.Instance.SetShieldMeterColor(gradientColor);
        }
    }
}