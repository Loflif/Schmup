using UnityEngine;

namespace Schmup
{
    public class Flail : MonoBehaviour, IWeapon
    {
        [SerializeField] private float SpinForce = 1.0f;
        [SerializeField] private float MaxSpinVelocity = 10.0f;

        [Header("Blur")] 
        [SerializeField] private float BlurSpinMaxVelocity = -180.0f;
        [SerializeField] private float AlphaFadeSpeed = 1.0f;
        
        private bool LastAttackInput = false;
        private Rigidbody2D FlailHead = null;

        private Transform AttachmentPoint = null;
        private Transform OwnTransform;

        private SpriteRenderer SpinBlur = null;

        private MeshRenderer[] Meshes;

        private void Awake()
        {
            Meshes = GetComponentsInChildren<MeshRenderer>();
            SpinBlur = GetComponentInChildren<SpriteRenderer>();
            OwnTransform = transform;
            FlailHead = GetComponentInChildren<Rigidbody2D>();
        }

        public void SetAttackInput(bool pIsAttackWanted)
        {
            LastAttackInput = pIsAttackWanted;
        }

        public void Attach(Transform pParent)
        {
            transform.parent = pParent;
            AttachmentPoint = pParent;
            SpinBlur.transform.position = pParent.position;
        }

        private void FixedUpdate()
        {
            if (LastAttackInput)
            {
                Spin();
                FadeBlur(true);
                FadeMeshes(false);
            }
            else
            {
                FadeBlur(false);
                FadeMeshes(true);
            }
            SpinFlailBlur();
        }

        private void Spin()
        {
            if (FlailHead.velocity.magnitude > MaxSpinVelocity)
                return;

            Vector3 directionToPlayer = (AttachmentPoint.position - FlailHead.transform.position).normalized;
            Vector3 perpendicularToPlayer = new Vector3(-directionToPlayer.y, directionToPlayer.x, 0);

            FlailHead.AddForce(perpendicularToPlayer * (SpinForce * Time.fixedDeltaTime));
        }

        private void FadeBlur(bool pFadeIn)
        {
            if (pFadeIn && SpinBlur.color.a >= 1 
            || !pFadeIn && SpinBlur.color.a <= 0)
                return;
            
            Color newColor = SpinBlur.color;
            
            float targetAlpha = pFadeIn ? 1 : 0;
        
            newColor.a = Mathf.SmoothStep(newColor.a, targetAlpha, AlphaFadeSpeed * Time.fixedDeltaTime);
            SpinBlur.color = newColor;
        }

        private void FadeMeshes(bool pFadeIn)
        {
            if (pFadeIn && Meshes[0].material.color.a >= 1
                || !pFadeIn && Meshes[0].material.color.a <= 0)
                return;

            Color newColor = Meshes[0].material.color;

            float targetAlpha = pFadeIn ? 1 : 0;
            
            newColor.a = Mathf.SmoothStep(newColor.a, targetAlpha, AlphaFadeSpeed * Time.fixedDeltaTime);
            foreach (MeshRenderer m in Meshes)
            {
                m.material.color = newColor;
            }
        }

        float GetVelocityPercent()
        {
            return FlailHead.velocity.magnitude / MaxSpinVelocity;
        }

        private void SpinFlailBlur()
        {
            SpinBlur.transform.Rotate(Vector3.forward, BlurSpinMaxVelocity * Time.fixedDeltaTime * GetVelocityPercent());
        }
    }
}

