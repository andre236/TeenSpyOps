using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixResolution : MonoBehaviour
{
    [SerializeField] private float _orthoSize = 5f;
    [SerializeField] private float _aspect = 1.66f;

    private List<CanvasScaler> _allCanvasScaler;

    private void Awake()
    {
        _allCanvasScaler = new List<CanvasScaler>();
        _allCanvasScaler.AddRange(FindObjectsOfType<CanvasScaler>());
    }

    void Update()
    {
        //Modify();
        RefreshAllCanvas();
    }

    private void RefreshAllCanvas()
    {

        foreach (CanvasScaler canvas in _allCanvasScaler)
        {
            if (Screen.width > 768)
                canvas.matchWidthOrHeight = 1;
            else
                canvas.matchWidthOrHeight = 0;

        }




    }

    private void Modify()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(-_orthoSize * _aspect, _orthoSize * _aspect, -_orthoSize, _orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);

    }


}
