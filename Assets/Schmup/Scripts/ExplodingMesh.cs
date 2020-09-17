using UnityEngine;

public class ExplodingMesh : MonoBehaviour
{
    [SerializeField] private float ExplosionForce = 500.0f;
    private Rigidbody[] Parts;
    private void Awake()
    {
        Parts = GetComponentsInChildren<Rigidbody>();
        SetRandomZRotation();
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
