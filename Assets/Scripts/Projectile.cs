using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private ViewportArea.ViewportPlane _viewportPlane;
    private Vector3 _velocity;

    public void Initialise(Camera targetCamera, Vector3 velocity)
    {
        _velocity = velocity;
        
        _viewportPlane = ViewportArea.CreateBoundingViewportPlane(targetCamera, gameObject,
            ViewportArea.ViewportLock.Center, 100, -10, ViewportArea.ViewportLock.Center, 100, -10);
    }

    private void Update()
    {
        gameObject.transform.position += _velocity * Time.deltaTime;
        
        if (_viewportPlane.IsOutOfViewportArea(gameObject.transform.position))
            Destroy(gameObject);
    }
}