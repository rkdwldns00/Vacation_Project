using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boost : MonoBehaviour
{
    private Rigidbody _playerRigid;
    private float _boostSpeed;
    private float _durationTime;

    private void Awake()
    {
        _playerRigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0)
        {
            Destroy(this);
        }
    }

    private void FixedUpdate()
    {
        Vector3 position = Vector3.zero;

        position.x = Mathf.Lerp(_playerRigid.position.x, 0, Time.fixedDeltaTime);
        position.z = _playerRigid.position.z + _boostSpeed * Time.fixedDeltaTime;
        _playerRigid.MovePosition(position);
    }

    public void SetBoostData(float boostSpeed, float durationTime)
    {
        if (_boostSpeed == 0) _boostSpeed = boostSpeed;
        _durationTime = durationTime;
    }
}
