using UnityEngine;

namespace Schmup
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private Vector2 ConstantVelocity = new Vector2(-2, 0);
        [SerializeField] private float CosFrequency = 5.0f;
        
        private void OnTriggerEnter2D(Collider2D pOther)
        {
            if(pOther.CompareTag("Player"))
            {
                Destroy(gameObject, 1.0f);
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            Vector3 currentVelocity = ConstantVelocity;
            currentVelocity += Vector3.up * Mathf.Cos(Time.time * CosFrequency);
            transform.position += currentVelocity * Time.deltaTime;
        }
    }   
}
