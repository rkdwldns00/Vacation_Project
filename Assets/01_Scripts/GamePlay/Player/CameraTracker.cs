using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    [SerializeField] private Vector3 _cameraStartPos;
    [SerializeField] private Vector3 _cameraTargetPos;
    [SerializeField] private Vector3 _cameraLookPos;
    [SerializeField] private float _startZoomTime;

    private Transform _playerTransform;
    private Vector3 _curruntOrigin => Vector3.forward * _playerTransform.position.z;
    private float _spawnedTime;

    private void Awake()
    {
        PlayerSpawner.Instance.OnSpawned += (player) =>
        {
            _playerTransform = player.transform;
        };
    }

    private void Start()
    {
        _spawnedTime = Time.time;
    }

    private void Update()
    {
        CameraTrackHandler();
    }

    private void CameraTrackHandler()
    {
        if (_playerTransform != null)
        {
            float rate = (Time.time - _spawnedTime) / _startZoomTime;
            transform.position = _curruntOrigin + Vector3.Lerp(_cameraStartPos, _cameraTargetPos, rate);
            transform.LookAt(_curruntOrigin + Vector3.Lerp(Vector3.zero, _cameraLookPos, rate));
        }
    }
}
