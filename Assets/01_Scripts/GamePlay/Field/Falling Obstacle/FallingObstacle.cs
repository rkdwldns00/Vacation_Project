using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : ObjectPoolable, IObstacleResetable
{
    [SerializeField] private GameObject _warringObjectPrefab;
    [SerializeField] private float _warringTime;
    [SerializeField] private GameObject _fallingObjectPrefab;
    [SerializeField] private GameObject _fallingObjectEffectPrefab;
    [SerializeField] private float _hitBoxRadius;
    private GameObject _fallingObject;

    bool isStarted = false;
    bool isHitted = false;

    public void ResetObstacle()
    {
        isStarted = false;
    }

    private void FixedUpdate()
    {
        if (!isStarted && Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _warringTime * Player.Instance.MoveSpeed)
        {
            isStarted = true;
            StartWarring();
        }

        if (_fallingObject != null && Player.Instance != null)
        {
            _fallingObject.transform.position = new Vector3(_fallingObject.transform.position.x,
                (_fallingObject.transform.position.z - Player.Instance.transform.position.z),
                _fallingObject.transform.position.z);

            if (!isHitted && _fallingObject.transform.position.z < Player.Instance.transform.position.z)
            {
                isHitted = true;

                Destroy(Instantiate(_fallingObjectEffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity), 2);
                if (CheckPlayerInHitBox())
                {
                    Player.Instance.TakeDamage();
                }
            }
        }
        else if (_fallingObject != null && Player.Instance == null)
        {
            Destroy(_fallingObject);
        }
    }

    public void StartWarring()
    {
        GameObject warningObject = Instantiate(_warringObjectPrefab, transform.position, Quaternion.identity);
        warningObject.GetComponent<Warring>().SetTime(_warringTime);
        Destroy(warningObject, 5);

        _fallingObject = Instantiate(_fallingObjectPrefab, transform.position + Vector3.up * (_fallingObjectPrefab.transform.position.z - Player.Instance.transform.position.z), Quaternion.identity);
        Destroy(_fallingObject, _warringTime + 3);
    }

    private bool CheckPlayerInHitBox()
    {
        if (Player.Instance == null) return false;

        Vector3 playerPos = Player.Instance.transform.position;
        playerPos.y = 0;
        Vector3 hitBoxPos = transform.position;
        hitBoxPos.y = 0;
        return Vector3.Distance(hitBoxPos, playerPos) < _hitBoxRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _hitBoxRadius);
    }
}
