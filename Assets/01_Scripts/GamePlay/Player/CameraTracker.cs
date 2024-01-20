using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraTracker : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] private Vector3 _cameraPos;
    [SerializeField] private Vector3 _cameraLookPos;

    private Transform _cameraTransform;

    private Vector3 _curruntOrigin => Vector3.forward * transform.position.z;

    private void Start()
    {
        _cameraTransform = Camera.main.transform;
        if (_cameraTransform is null)
        {
            Debug.LogWarning("카메라가 존재하지 않습니다!");
            Destroy(this);
        }
    }

    private void Update()
    {
        CameraTrackHandler();
    }

    private void CameraTrackHandler()
    {
        if (_cameraTransform != null)
        {
            _cameraTransform.position = _curruntOrigin + _cameraPos;
            _cameraTransform.LookAt(_curruntOrigin + _cameraLookPos);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_curruntOrigin, _curruntOrigin + _cameraPos);
        Gizmos.DrawSphere(_curruntOrigin + _cameraPos, 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_curruntOrigin + _cameraPos, _curruntOrigin + _cameraLookPos);
    }
}
