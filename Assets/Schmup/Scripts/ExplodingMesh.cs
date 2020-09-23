using System.Collections.Generic;
using UnityEngine;

namespace Schmup
{
    public class ExplodingMesh : MonoBehaviour
    {
        [SerializeField] private float ExplosionForce = 500.0f;
        [SerializeField] private float ExplosionRadius = 1.0f;
        [SerializeField] private float ExplosionDelay = 0.0f;
        private Rigidbody[] Parts;
        private List<Vector3> InitialPartPositions = new List<Vector3>();
        private List<Quaternion> InitialPartRotations = new List<Quaternion>();

        private void Awake()
        {
            SetRandomZRotation();
            Parts = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody r in Parts)
            {
                InitialPartPositions.Add(r.transform.position);
                InitialPartRotations.Add(r.transform.rotation);
            }

            if (ExplosionDelay > 0) // So the meshes don't appear until they actually explode,
                                    // not important if they're supposed to explode instantly
            {
                DisableMeshes();
            }
        }

        private void SetRandomZRotation()
        {
            transform.Rotate(Vector3.forward, Random.Range(0, 360));
        }

        private void OnEnable()
        {
            Invoke("Explode", ExplosionDelay);
        }

        private void OnDisable()
        {
            ResetParts();
        }

        private void EnableMeshes()
        {
            foreach (Rigidbody r in Parts)
            {
                r.gameObject.SetActive(true);
            }
        }

        private void DisableMeshes()
        {
            foreach (Rigidbody r in Parts)
            {
                r.gameObject.SetActive(false);
            }
        }

        private void Explode()
        {
            EnableMeshes();
            foreach (Rigidbody r in Parts)
            {
                r.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);
            }
        }

        private void ResetParts()
        {
            for (int i = 0; i < Parts.Length; i++)
            {
                Rigidbody part = Parts[i];
                part.position = InitialPartPositions[i];
                part.rotation = InitialPartRotations[i];
            }
        }
    }
}