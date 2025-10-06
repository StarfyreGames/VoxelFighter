using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RollController : MonoBehaviour
{
    public float rollRate = 1;
    public float maxBank = 50;

    private Rigidbody _rigidbody;

    private Quaternion _originalRotation;
    private Quaternion _targetRollRotation;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _originalRotation = _rigidbody.rotation;
        _targetRollRotation = _originalRotation;
    }

    private void FixedUpdate()
    {
        Quaternion newRotation = Quaternion.Lerp(
            _rigidbody.rotation,
            _targetRollRotation,
            Time.fixedDeltaTime * rollRate
        );

        _rigidbody.MoveRotation(newRotation);
    }

    void OnMove(InputValue input)
    {
        var dir = input.Get<Vector3>();
        var corrected = new Vector3(dir.z, 0, dir.x * -1f); // Map translation to the corresponding rotation
        _targetRollRotation = Quaternion.Euler(corrected * maxBank) * _originalRotation;
    }
}