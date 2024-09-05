using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : ObjectPoolable, IObstacleResetable
{
    [SerializeField] private Transform _barrelObject;
    [SerializeField] private Transform _warningObject;
    [SerializeField] private float _minBarrelSpeed;
    [SerializeField] private float _maxBarrelSpeed;
    [SerializeField] private float _startTime;
    [SerializeField] private float _warningTime;
    [SerializeField] private float _startXPos;

    private float _barrelSpeed;
    private bool _isBarrelStartLeft;
    private bool _isPlayedWarningObject;

    public void ResetObstacle()
    {
        _isBarrelStartLeft = Random.Range(0, 2) == 0;
        float startXPos = _startXPos * (_isBarrelStartLeft ? -1 : 1);
        _barrelObject.localPosition = new Vector3(startXPos, 0, 0);
        _warningObject.localPosition = new Vector3(startXPos, 0, 0);

        _barrelSpeed = Random.Range(_minBarrelSpeed, _maxBarrelSpeed);

        _isPlayedWarningObject = false;
    }

    private void FixedUpdate()
    {
        if (Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _startTime * Player.Instance.OriginMoveSpeed)
        {
            Vector3 moveDir = _isBarrelStartLeft ? Vector3.right : Vector3.left;
            _barrelObject.Translate(moveDir * _barrelSpeed * Time.fixedDeltaTime);
        }

        if (Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _warningTime * Player.Instance.OriginMoveSpeed && !_isPlayedWarningObject)
        {
            _warningObject.GetComponentInChildren<ParticleSystem>().Play();
            _isPlayedWarningObject = true;
        }
    }
}
