using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _laserObject;
    [SerializeField] private GameObject _warningObject;
    [SerializeField] private float _laserMoveSpeed;
    [SerializeField] private float _startTime;
    [SerializeField] private float _startXPos;

    private bool _isLaserStartLeft;
    private bool _isLaserActivated = false;

    private void Awake()
    {
        _isLaserStartLeft = Random.Range(0, 2) == 0;
        float startXPos = _startXPos * (_isLaserStartLeft ? -1 : 1);
        transform.position = new Vector3(startXPos, 0, transform.position.z);
    }

    private void FixedUpdate()
    {
        if (!_isLaserActivated && Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _startTime * Player.Instance.OriginMoveSpeed)
        {
            StartCoroutine(LaserActiveCoroutine());
            _isLaserActivated = true;
        }

        if (_isLaserActivated)
        {
            float targetZPos = Player.Instance ? Player.Instance.transform.position.z + _startTime * Player.Instance.OriginMoveSpeed : 0;
            transform.position = new Vector3(transform.position.x, transform.position.y, targetZPos);
        }
    }

    private IEnumerator LaserActiveCoroutine()
    {
        _warningObject.SetActive(true);
        yield return new WaitForSeconds(1);
        _laserObject.SetActive(true);

        float targetXPos = _isLaserStartLeft ? _startXPos : -_startXPos;

        while (transform.position.x != targetXPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetXPos, 0, transform.position.z), _laserMoveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        Destroy(gameObject);

        yield break;
    }
}
