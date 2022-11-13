using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Ultilities;

public class CameraHelper : GenericSingleton<CameraHelper>
{
    private Camera _camera;

    [HideInInspector]
    public Vector2 HalfSize;
    [HideInInspector]
    public Vector2 FullSize;

    [HideInInspector]
    public float LowerBound;
    [HideInInspector]
    public float UpperBound;
    [HideInInspector]
    public float LeftBound;
    [HideInInspector]
    public float RightBound;

    private void Start()
    {
        _camera = Camera.main;

        CalculateOrthographicCameraProperties();
    }

    private void CalculateOrthographicCameraProperties()
    {
        float halfHeight = _camera.orthographicSize;
        float halfWidth = halfHeight * _camera.aspect;

        HalfSize = new(halfWidth, halfHeight);

        float fullHeight = _camera.orthographicSize * 2f;
        float fullWidth = fullHeight * _camera.aspect;

        FullSize = new(fullWidth, fullHeight);

        LowerBound = _camera.transform.position.y - halfHeight;
        UpperBound = _camera.transform.position.y + halfHeight;
        LeftBound = _camera.transform.position.x - halfWidth;
        RightBound = _camera.transform.position.x + halfWidth;
    }
}
