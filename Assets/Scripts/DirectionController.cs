using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionController : MonoBehaviour
{
    // Just a static offset to make sure the magnitude is reasonable
    private const int PowerDelta = 100_000;
    
    public float power = 1;

    private Rigidbody _rigidBody;
    private Vector3 _forceVector;

    private float ForceMagnitude => power * Time.fixedDeltaTime * PowerDelta;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        // Give a shove in the right direction
        _rigidBody.AddForce(_forceVector * power, ForceMode.Impulse);
    }

    void OnMove(InputValue input)
    {
        _forceVector = input.Get<Vector3>();
    }
}