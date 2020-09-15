using System;
using UnityEngine;

public class ExplodingMesh : MonoBehaviour
{
    [SerializeField] private float ExplosionForce = 500.0f;
    private Rigidbody[] Parts;
    private void Awake()
    {
        Parts = GetComponentsInChildren<Rigidbody>();
    }

    private void OnEnable()
    {
        
        foreach (Rigidbody r in Parts)
        {
            r.AddExplosionForce(ExplosionForce, transform.position, 1);
        }
    }
}
