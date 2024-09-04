using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
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

    private void Awake()
    {
        _isBarrelStartLeft = Random.Range(0, 2) == 0;
        float startXPos = _startXPos * (_isBarrelStartLeft ? -1 : 1);
        _barrelObject.localPosition = new Vector3(startXPos, 0, 0);
        _warningObject.localPosition = new Vector3(startXPos, 0, 0);

        _barrelSpeed = Random.Range(_minBarrelSpeed, _maxBarrelSpeed);
    }

    private void Update()
    {
        if (Player.Instance != null && transform.position.z + 20 < Player.Instance.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _startTime * Player.Instance.OriginMoveSpeed)
        {
            Vector3 moveDir = _isBarrelStartLeft ? Vector3.right : Vector3.left;
            _barrelObject.Translate(moveDir * _barrelSpeed * Time.fixedDeltaTime);
        }

        if (Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _warningTime * Player.Instance.OriginMoveSpeed)
        {
            if (!_warningObject.gameObject.activeSelf)
            {
                _warningObject.gameObject.SetActive(true);
            }
        }
    }
}
