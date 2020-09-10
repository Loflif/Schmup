using UnityEngine;

namespace Schmup
{
    public class Flail : MonoBehaviour, IWeapon
    {
        [SerializeField] private float SpinForce = 1.0f;
        [SerializeField] private float MaxSpinVelocity = 10.0f;

        [Header("Blur")] 
        [SerializeField] private float BlurAlphaMaxMultiplier = 0.7f;
        [SerializeField] private float BlurSpinMaxVelocity = -180.0f;
        
        private bool LastAttackInput = false;
        private Rigidbody2D FlailHead = null;

        private Transform AttachmentPoint = null;
        private Transform OwnTransform;

        private SpriteRenderer SpinBlur = null;

        private void Awake()
        {
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
            }
            ChangeSpinBlurAlphaBasedOnFlailVelocity();
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

        float GetVelocityPercent()
        {
            return FlailHead.velocity.magnitude / MaxSpinVelocity;
        }

        private void ChangeSpinBlurAlphaBasedOnFlailVelocity()
        {
            Color newColor = SpinBlur.color;
            
            newColor.a = Mathf.SmoothStep(newColor.a, GetVelocityPercent() - BlurAlphaMaxMultiplier, 0.5f);
            SpinBlur.color = newColor;
        }

        private void SpinFlailBlur()
        {
            SpinBlur.transform.Rotate(Vector3.forward, BlurSpinMaxVelocity * Time.fixedDeltaTime * GetVelocityPercent());
        }
    }
}

