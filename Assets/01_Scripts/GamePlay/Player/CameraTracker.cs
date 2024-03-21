using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraStartPos;
    [SerializeField] private Vector3 _cameraTargetPos;
    [SerializeField] private Vector3 _cameraLookPos;

    private Transform _playerTransform;
    private Vector3 _curruntOrigin => Vector3.forward * _playerTransform.position.z;

    private void Awake()
    {
        PlayerSpawner.Instance.OnSpawned += (player) =>
        {
            _playerTransform = player.transform;
        };
    }

    private void Update()
    {
        CameraTrackHandler();
    }

    private void CameraTrackHandler()
    {
        if (_playerTransform != null)
        {
            transform.position = _curruntOrigin + _cameraTargetPos;
            transform.LookAt(_curruntOrigin + _cameraLookPos);
        }
    }
}
