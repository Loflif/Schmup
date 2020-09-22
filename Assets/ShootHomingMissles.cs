using UnityEngine;

namespace Schmup
{
    public class ShootHomingMissles : MonoBehaviour
    {
        [SerializeField] private GameObject Missile = null;
        [SerializeField] private float MissileCooldown = 2.0f;
        [SerializeField] private Transform MissileLaunchPoint = null;

        private float Timer = 0;
        private void Update()
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                FireMissile();
            }
        }

        private void FireMissile()
        {
            Instantiate(Missile, MissileLaunchPoint.position, MissileLaunchPoint.rotation);
            Timer = MissileCooldown;
        }
    }   
}
