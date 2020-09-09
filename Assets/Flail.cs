using System;
using Schmup;
using UnityEngine;

public class Flail : MonoBehaviour, IWeapon
{
    [SerializeField] private float SpinForce = 500.0f;

    private bool LastAttackInput = false;
    private Rigidbody FlailHead = null;

    private void Awake()
    {
        FlailHead = GetComponentInChildren<Rigidbody>();
    }

    public void SetAttackInput(bool pIsAttackWanted)
    {
        LastAttackInput = pIsAttackWanted;
    }

    private void FixedUpdate()
    {
        if (LastAttackInput)
        {
            Spin();
        }
    }

    private void Spin()
    {
        // FlailHead.AddForce();
        
    }
}
