using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    [SerializeField] private float _xRatio;
    [SerializeField] private float _yRatio;

    private void Awake()
    {
        SetResolution();
    }

    private void SetResolution()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;

        float scaleHeight = ((float)Screen.width / Screen.height) / (_xRatio / _yRatio);
        float scaleWidth = 1f / scaleHeight;

        if (scaleHeight < 1)
        {
            rect.height = scaleHeight;
            rect.y = (1f - scaleHeight) / 2f;
        }
        else
        {
            rect.width = scaleWidth;
            rect.x = (1f - scaleWidth) / 2f;
        }

        camera.rect = rect;
    }
}
