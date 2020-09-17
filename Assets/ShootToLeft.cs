using UnityEngine;

public class ShootToLeft : MonoBehaviour
{
    [SerializeField] private float ShotDistance = 5.0f;
    [SerializeField] private LayerMask TargetTable;

    private ParticleSystem Weapon = null;    
    private Transform OwnTransform;

    private void Awake()
    {
        OwnTransform = transform;

        Weapon = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if (PlayerIsAhead())
        {
            Shoot();
        }
        else
        {
            StopShooting();
        }
    }

    private bool PlayerIsAhead()
    {
        RaycastHit2D playerHitRay = Physics2D.Raycast(OwnTransform.position, Vector2.left, ShotDistance, TargetTable);
        Debug.DrawRay(OwnTransform.position, Vector2.left * ShotDistance, playerHitRay ? Color.green : Color.red );
        return playerHitRay;
    }

    private void Shoot()
    {
        if (Weapon.isPlaying)
            return;
        Weapon.Play();
    }

    private void StopShooting()
    {
        Weapon.Stop();
    }
}
