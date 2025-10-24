using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using utils;

public class ViewportArea : MonoBehaviour
{
    public bool visualise = false;
    public Camera targetCamera;

    public ViewportLock horizontalLock;
    public int widthPercent;
    public float widthInset;

    public ViewportLock verticalLock;
    public int heightPercent;
    public float heightInset;

    private ViewportPlane _viewportPlane;

    private void Awake()
    {
        UpdateViewPlane();
    }

    private void Update()
    {
        if (visualise) UpdateViewPlane();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(_viewportPlane.TopLeft, _viewportPlane.BottomLeft);
        Gizmos.DrawLine(_viewportPlane.TopRight, _viewportPlane.BottomRight);
        Gizmos.DrawLine(_viewportPlane.BottomRight, _viewportPlane.BottomLeft);
        Gizmos.DrawLine(_viewportPlane.TopRight, _viewportPlane.TopLeft);
    }

    private void UpdateViewPlane()
    {
        _viewportPlane = ViewportPlane.CreateBoundingViewportPlane(targetCamera, gameObject, horizontalLock,
            widthPercent, widthInset, verticalLock, heightPercent, heightInset);
    }


    // ====================
    // ==== PUBLIC API ====
    // ====================

    public Vector3 Clamp(Vector3 position)
    {
        return _viewportPlane.Clamp(position);
    }


    public bool IsInsideViewportArea(Vector3 position)
    {
        return _viewportPlane.IsInsideViewportArea(position);
    }

    public bool IsOutOfViewportArea(Vector3 position)
    {
        return _viewportPlane.IsOutOfViewportArea(position);
    }
}