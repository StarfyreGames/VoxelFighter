using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class DirectionController : MonoBehaviour
{
    public float power = 1;
    public float leftLimit;
    public float rightLimit;
    public float upperLimit;
    public float lowerLimit;

    private Rigidbody _rigidBody;
    private Vector3 _forceVector;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (WithinBoundingBox(_rigidBody))
        {
            // Give a shove in the right direction
            _rigidBody.AddForce(_forceVector * power, ForceMode.Impulse);
        }
        else
        {
            // Stop the ship from trying to move further
            _rigidBody.linearVelocity = Vector3.zero;
        }
    }

    private bool WithinBoundingBox(Rigidbody rb)
    {
        var position = transform.position;

        var isLeftHit = position.x < leftLimit;
        if (isLeftHit) rb.position = new Vector3(leftLimit, position.y, position.z);

        var isRightHit = position.x > rightLimit;
        if (isRightHit) rb.position = new Vector3(rightLimit, position.y, position.z);

        var isUpperHit = position.z > upperLimit;
        if (isUpperHit) rb.position = new Vector3(position.x, position.y, upperLimit);

        var isLowerHit = position.z < lowerLimit;
        if (isLowerHit) rb.position = new Vector3(position.x, position.y, lowerLimit);
        
        return !isLowerHit && !isUpperHit && !isLowerHit && !isRightHit;
    }

    void OnMove(InputValue input)
    {
        _forceVector = input.Get<Vector3>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        UnityEngine.Debug.Log("HERE");
    }
}