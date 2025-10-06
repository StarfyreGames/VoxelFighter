using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionController : MonoBehaviour
{
    public float power = 1;

    private Rigidbody _rigidBody;
    private Vector3 _forceVector;


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