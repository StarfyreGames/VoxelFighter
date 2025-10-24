using UnityEngine;
using utils;

public class Projectile : MonoBehaviour
{
    private Vector3 _velocity;

    public void Initialise(Camera targetCamera, Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void Update()
    {
        gameObject.transform.position += _velocity * Time.deltaTime;
        if (CleanUpFence.ShouldDestroy(gameObject)) Destroy(gameObject);
    }
}