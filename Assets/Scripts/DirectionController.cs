using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectionController : MonoBehaviour
{
    public float power = 100;
    public float maxSpeed = 50;

    private Rigidbody _rigidBody;
    private Vector3 _forceVector;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.maxLinearVelocity = maxSpeed;
    }

    private void FixedUpdate()
    {
        // Give a shove in the right direction
        _rigidBody.AddForce(_forceVector * power, ForceMode.Acceleration);
    }

    void OnMove(InputValue input)
    {
        _forceVector = input.Get<Vector3>();
    }
}