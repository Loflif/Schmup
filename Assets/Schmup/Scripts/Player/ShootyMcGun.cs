using UnityEngine;

namespace Schmup
{
    public class ShootyMcGun : MonoBehaviour, IWeapon
    {
        [SerializeField] private float ShotPathDistance = 20.0f;
        
        private Transform OwnTransform = null;
        private LineRenderer ShotPath = null;
        private MeshRenderer Barrel = null;
        private ParticleSystem ShotEmission = null;

        private bool IsAttached = false;

        private void Awake()
        {
            OwnTransform = transform;
            Barrel = GetComponentInChildren<MeshRenderer>();
            ShotEmission = GetComponentInChildren<ParticleSystem>();
            ShotPath = GetComponentInChildren<LineRenderer>();
        }

        public void Attach(Transform pParent)
        {
            OwnTransform.parent = pParent;
            OwnTransform.position = pParent.position;
            OwnTransform.rotation = pParent.rotation;
            IsAttached = true;
        }

        public void Toggle(bool pActive)
        {
            Barrel.gameObject.SetActive(pActive);
            ShotPath.gameObject.SetActive(pActive);
        }

        public void SetAttackInput(bool pIsAttackWanted)
        {
            if (pIsAttackWanted)
            {
                StartShooting();
            }
            else
            {
                StopShooting();
            }
        }

        public void Aim(Vector2 pAimDirection)
        {
            OwnTransform.right = pAimDirection;
        }

        private void FixedUpdate()
        {
            if(IsAttached)
                DrawShotPath();
        }

        private void StartShooting()
        {
            ShotEmission.Play();
        }

        private void StopShooting()
        {
            ShotEmission.Stop();
        }

        private void DrawShotPath()
        {
            Vector3 gunMuzzlePosition = ShotPath.transform.position;
            ShotPath.SetPosition(0, gunMuzzlePosition);
            ShotPath.SetPosition(1, gunMuzzlePosition + ShotPath.transform.right * ShotPathDistance);
        }
    }
}
