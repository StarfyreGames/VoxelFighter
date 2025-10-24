using UnityEngine;
using utils;

public class Projectile : MonoBehaviour
{
    private ViewportPlane _viewportPlane;
    private Vector3 _velocity;

    public void Initialise(Camera targetCamera, Vector3 velocity)
    {
        _velocity = velocity;

        _viewportPlane = ViewportPlane.CreateBoundingViewportPlane(targetCamera, gameObject, ViewportLock.Center, 100,
            -10, ViewportLock.Center, 100, -10);
    }

    private void Update()
    {
        gameObject.transform.position += _velocity * Time.deltaTime;

        if (_viewportPlane.IsOutOfViewportArea(gameObject.transform.position))
            Destroy(gameObject);
    }
}