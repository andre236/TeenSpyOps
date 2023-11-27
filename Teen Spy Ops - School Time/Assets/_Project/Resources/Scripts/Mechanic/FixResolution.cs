using UnityEngine;

public class FixResolution : MonoBehaviour
{
    [SerializeField] private float _orthoSize = 5f;
    [SerializeField] private float _aspect = 1.66f;


    // Update is called once per frame
    void Update()
    {
        Camera.main.projectionMatrix = Matrix4x4.Ortho(-_orthoSize * _aspect, _orthoSize * _aspect, -_orthoSize, _orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
    }



}
