using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShield : MonoBehaviour
{
    private GameObject _instantiatedObstacleShieldEffect;
    private float _durationTime;

    private void Update()
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0)
        {
            Destroy(_instantiatedObstacleShieldEffect);
            Destroy(this);
        }
    }

    public void SetObstacleShieldData(float durationTime, GameObject obstacleShieldEffectPrefab)
    {
        if (_durationTime < durationTime)
        {
            _durationTime = durationTime;
        }

        if (_instantiatedObstacleShieldEffect == null)
        {
            _instantiatedObstacleShieldEffect = Instantiate(obstacleShieldEffectPrefab, transform);
            _instantiatedObstacleShieldEffect.transform.localPosition = Vector3.zero;
        }
    }
}
