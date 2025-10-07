using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.Shapes;

public class DirectionController : MonoBehaviour
{
    public float power = 1;

    private ViewportArea _viewportArea;
    private Rigidbody _rigidBody;
    private Vector3 _forceVector;

    private void Awake()
    {
        _viewportArea = GetComponent<ViewportArea>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Give a shove in the right direction
        _rigidBody.AddForce(_forceVector * power, ForceMode.Impulse);

        // Clamp the position
        var original = _rigidBody.position;
        var update = _viewportArea.Clamp(original);
        
        // If there is no change, there is nothing to update
        if (original == update) return;
        
        // Update the position
        _rigidBody.position = update;

        // Make sure the ship doesn't hold its momentum if it's on the wall
        var velocity = _rigidBody.linearVelocity;
        if (!Mathf.Approximately(original.x, update.x))
            _rigidBody.linearVelocity = new Vector3(0, velocity.y, velocity.z);

        if (!Mathf.Approximately(original.z, update.z))
            _rigidBody.linearVelocity = new Vector3(velocity.x, velocity.y, 0);
    }

    void OnMove(InputValue input)
    {
        _forceVector = input.Get<Vector3>();
    }
}