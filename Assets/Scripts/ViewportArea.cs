using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;

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
        ComputeLimits();
    }

    private void Update()
    {
        if (visualise) ComputeLimits();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(_viewportPlane.TopLeft, _viewportPlane.BottomLeft);
        Gizmos.DrawLine(_viewportPlane.TopRight, _viewportPlane.BottomRight);
        Gizmos.DrawLine(_viewportPlane.BottomRight, _viewportPlane.BottomLeft);
        Gizmos.DrawLine(_viewportPlane.TopRight, _viewportPlane.TopLeft);
    }

    private void ComputeLimits()
    {
        var distance = Vector3.Distance(targetCamera.transform.position, gameObject.transform.position);

        var tl = targetCamera.ViewportToWorldPoint(new Vector3(0, 1, distance));
        var tr = targetCamera.ViewportToWorldPoint(new Vector3(1, 1, distance));
        var br = targetCamera.ViewportToWorldPoint(new Vector3(1, 0, distance));
        var bl = targetCamera.ViewportToWorldPoint(new Vector3(0, 0, distance));

        _viewportPlane = new ViewportPlane(tl, tr, bl, br)
            .AddPadding(widthInset, heightInset)
            .ResizeHeight(verticalLock, heightPercent)
            .ResizeWidth(horizontalLock, widthPercent);
    }

    public Vector3 Clamp(Vector3 position)
    {
        return _viewportPlane.Clamp(position);
    }

    // ====================
    // ==== PUBLIC API ====
    // ====================

    // public float Width() => Vector3.Distance(_topLeftCorner, _topRightCorner);
    // public float Height() => Vector3.Distance(_topRightCorner, _bottomRightCorner);
    //
    // public bool IsOobLeft(Vector3 position)
    // {
    //     return position.x < _topLeftCorner.x;
    // }
    //
    // public bool IsOobRight(Vector3 position)
    // {
    //     return position.x > _topRightCorner.x;
    // }
    //
    // public bool IsOobTop(Vector3 position)
    // {
    //     return position.z > _topRightCorner.z;
    // }
    //
    // public bool IsOobBottom(Vector3 position)
    // {
    //     return position.z < _bottomRightCorner.z;
    // }
    //
    // public bool IsOobVertical(Vector3 position)
    // {
    //     return IsOobBottom(position) || IsOobTop(position);
    // }
    //
    // public bool IsOobHorizontal(Vector3 position)
    // {
    //     return IsOobLeft(position) || IsOobRight(position);
    // }
    //
    // public Vector3 ClampHorizontally(Vector3 position)
    // {
    //     if (IsOobLeft(position)) return new Vector3(_topLeftCorner.x, position.y, position.z);
    //     if (IsOobRight(position)) return new Vector3(_topRightCorner.x, position.y, position.z);
    //     return position;
    // }
    //
    // public Vector3 ClampVertically(Vector3 position)
    // {
    //     if (IsOobTop(position)) return new Vector3(position.x, position.y, _topRightCorner.z);
    //     if (IsOobBottom(position)) return new Vector3(position.x, position.y, _bottomLeftCorner.z);
    //     return position;
    // }


    public enum ViewportLock
    {
        Start,
        Center,
        End
    }

    private class ViewportPlane
    {
        private Vector3 _topLeft;
        private Vector3 _topRight;
        private Vector3 _bottomLeft;
        private Vector3 _bottomRight;

        public Vector3 TopLeft => _topLeft;
        public Vector3 TopRight => _topRight;
        public Vector3 BottomLeft => _bottomLeft;
        public Vector3 BottomRight => _bottomRight;

        public ViewportPlane(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft,
            Vector3 bottomRight)
        {
            _topLeft = topLeft;
            _topRight = topRight;
            _bottomLeft = bottomLeft;
            _bottomRight = bottomRight;
        }

        public ViewportPlane AddPadding(float horizontal, float vertical)
        {
            AddLeftPadding(horizontal);
            AddRightPadding(horizontal);

            AddTopPadding(vertical);
            AddBottomPadding(vertical);

            return this;
        }

        public ViewportPlane ResizeHeight(ViewportLock viewportLock, int heightPercent)
        {
            var offset = PercentageOfDistance(_topLeft, _bottomLeft, heightPercent);
            ApplyLockPositioning(AddTopPadding, AddBottomPadding, viewportLock, offset);

            return this;
        }

        public ViewportPlane ResizeWidth(ViewportLock viewportLock, int widthPercent)
        {
            var offset = PercentageOfDistance(_topRight, _topLeft, widthPercent);
            ApplyLockPositioning(AddLeftPadding, AddRightPadding, viewportLock, offset);

            return this;
        }

        public Vector3 Clamp(Vector3 position)
        {
            var outLeft = position.x < TopLeft.x;
            if (outLeft) position.x = TopLeft.x;

            var outRight = position.x > TopRight.x;
            if (outRight) position.x = TopRight.x;

            var outTop = position.z > TopLeft.z;
            if (outTop) position.z = TopLeft.z;

            var outBottom = position.z < BottomRight.z;
            if (outBottom) position.z = BottomRight.z;

            return position;
        }

        private void AddLeftPadding(float offset)
        {
            _topLeft.x += offset;
            _bottomLeft.x += offset;
        }

        private void AddRightPadding(float offset)
        {
            _topRight.x -= offset;
            _bottomRight.x -= offset;
        }

        private void AddBottomPadding(float offset)
        {
            _bottomLeft.z += offset;
            _bottomRight.z += offset;
        }

        private void AddTopPadding(float offset)
        {
            _topLeft.z -= offset;
            _topRight.z -= offset;
        }

        private static float PercentageOfDistance(Vector3 from, Vector3 to, float percentage)
        {
            var length = Vector3.Distance(from, to);
            return length - length * (percentage / 100);
        }

        private static void ApplyLockPositioning(Action<float> addStartPadding, Action<float> addEndPadding,
            ViewportLock viewportLock,
            float offset)
        {
            switch (viewportLock)
            {
                case ViewportLock.Start:
                    addEndPadding(offset);
                    break;

                case ViewportLock.Center:
                    addStartPadding(offset / 2);
                    addEndPadding(offset / 2);
                    break;

                case ViewportLock.End:
                    addStartPadding(offset);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(viewportLock), viewportLock, null);
            }
        }
    }
}