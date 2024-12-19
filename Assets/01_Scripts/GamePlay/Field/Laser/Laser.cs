using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : ObjectPoolable, IObstacleResetable
{

    [Header("Vertical Laser")]
    [SerializeField] private GameObject _frontLaserPointer;
    [SerializeField] private GameObject _frontLaserBeam;
    [SerializeField] private ParticleSystem _frontLaserWarning;
    [SerializeField] private float _minFrontLaserMoveSpeed;
    [SerializeField] private float _maxFrontLaserMoveSpeed;
    [SerializeField] private float _frontLaserStartXPos;
    private bool _isFrontLaserEnd;

    [Header("Horizontal Laser")]
    [SerializeField] private GameObject _rightLaserPointer;
    [SerializeField] private GameObject _rightLaserBeam;
    [SerializeField] private ParticleSystem _rightLaserWarning;
    [SerializeField] private float _minRightLaserMoveSpeed;
    [SerializeField] private float _maxRightLaserMoveSpeed;
    [SerializeField] private float _rightLaserStartZPos;
    private bool _isRightLaserEnd;

    private bool _isLaserActivated = false;

    public void ResetObstacle()
    {
        _isLaserActivated = false;
        _isFrontLaserEnd = false;
        _isRightLaserEnd = false;

        float frontLaserStartXPos = _frontLaserStartXPos * (Random.Range(0, 2) == 0 ? -1 : 1);
        float rightLaserStartZPos = _rightLaserStartZPos * (Random.Range(0, 2) == 0 ? -1 : 1);

        _frontLaserPointer.transform.localPosition = new Vector3(frontLaserStartXPos, 1, _frontLaserPointer.transform.localPosition.z);
        _rightLaserPointer.transform.localPosition = new Vector3(_rightLaserPointer.transform.localPosition.x, 1, rightLaserStartZPos);
    }

    private void FixedUpdate()
    {
        if (!_isLaserActivated && Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z)
        {
            StartCoroutine(LaserActiveCoroutine());
            StartCoroutine(RightLaserActiveCoroutine());
            _isLaserActivated = true;
        }

        if (_isLaserActivated)
        {
            float targetZPos = Player.Instance ? Player.Instance.transform.position.z : transform.position.z;
            transform.position = new Vector3(transform.position.x, transform.position.y, targetZPos);

            if (_isFrontLaserEnd && _isRightLaserEnd)
            {
                _frontLaserBeam.SetActive(false);
                _rightLaserBeam.SetActive(false);
                ReleaseObject();
            }
        }
    }

    private IEnumerator LaserActiveCoroutine()
    {
        _frontLaserWarning.Play();
        yield return new WaitForSeconds(1);
        _frontLaserBeam.SetActive(true);

        float targetXPos = _frontLaserPointer.transform.localPosition.x < 0 ? _frontLaserStartXPos : -_frontLaserStartXPos;
        float laserSpeed = Random.Range(_minFrontLaserMoveSpeed, _maxFrontLaserMoveSpeed) * Player.Instance.MoveSpeedRate;

        while (_frontLaserPointer.transform.localPosition.x != targetXPos)
        {
            _frontLaserPointer.transform.localPosition = 
                Vector3.MoveTowards(_frontLaserPointer.transform.localPosition, new Vector3(targetXPos, 1, _frontLaserPointer.transform.localPosition.z), laserSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        _isFrontLaserEnd = true;

        yield break;
    }

    private IEnumerator RightLaserActiveCoroutine()
    {
        _rightLaserWarning.Play();
        yield return new WaitForSeconds(1);
        _rightLaserBeam.SetActive(true);

        float targetZPos = _rightLaserPointer.transform.localPosition.z < 0 ? _rightLaserStartZPos : -_rightLaserStartZPos;
        float laserSpeed = Random.Range(_minRightLaserMoveSpeed, _maxRightLaserMoveSpeed) * Player.Instance.MoveSpeedRate;

        while ( _rightLaserPointer.transform.localPosition.z != targetZPos)
        {
            _rightLaserPointer.transform.localPosition = 
                Vector3.MoveTowards(_rightLaserPointer.transform.localPosition, new Vector3(_rightLaserPointer.transform.localPosition.x, 1, targetZPos), laserSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);
        _isRightLaserEnd = true;

        yield break;
    }
}
