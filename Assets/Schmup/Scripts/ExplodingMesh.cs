using System.Collections.Generic;
using UnityEngine;

namespace Schmup
{
    public class ExplodingMesh : MonoBehaviour
    {
        [SerializeField] private float ExplosionForce = 500.0f;
        private Rigidbody[] Parts;
        private List<Vector3> PartPositions = new List<Vector3>();
        private List<Quaternion> PartRotations = new List<Quaternion>();
        private void Awake()
        {
            SetRandomZRotation();
            Parts = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody r in Parts)
            {
                PartPositions.Add(r.transform.position);
                PartRotations.Add(r.transform.rotation);
            }
        }

        private void SetRandomZRotation()
        {
            transform.Rotate(Vector3.forward, Random.Range(0, 360));
        }
    
        private void OnEnable()
        {
            foreach (Rigidbody r in Parts)
            {
                r.AddExplosionForce(ExplosionForce, transform.position, 1);
            }
        }
    }
}
