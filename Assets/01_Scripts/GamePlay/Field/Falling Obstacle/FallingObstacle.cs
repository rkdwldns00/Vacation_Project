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

    bool isStarted = false;

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
    }

    public void StartWarring()
    {
        float fallingObjectHeight = -Physics.gravity.y * _warringTime / 2 * _warringTime;

        GameObject warningObject = Instantiate(_warringObjectPrefab, transform.position, Quaternion.identity);
        warningObject.GetComponent<Warring>().SetTime(_warringTime);
        Destroy(warningObject, 5);

        Destroy(Instantiate(_fallingObjectPrefab, transform.position + Vector3.up * fallingObjectHeight, Quaternion.identity),_warringTime + 3);

        StartCoroutine(HitDelay());
    }

    private IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(_warringTime);

        Destroy(Instantiate(_fallingObjectEffectPrefab, new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity), 2);

        if (CheckPlayerInHitBox())
        {
            Debug.Log("Damaged");
            Player.Instance.TakeDamage();
        }
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
