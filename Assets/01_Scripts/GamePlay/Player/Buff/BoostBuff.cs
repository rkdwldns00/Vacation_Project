using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoostBuff : Buff
{
    private Rigidbody _playerRigid;
    private float _boostSpeed;
    private float _durationTime;

    public BoostBuff(float boostSpeed, float durationTime)
    {
        _boostSpeed = boostSpeed;
        _durationTime = durationTime;
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        _playerRigid = buffSystem.GetComponent<Rigidbody>();
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0) buffSystem.RemoveBuff(this);

        Vector3 position = Vector3.zero;

        position.x = Mathf.Lerp(_playerRigid.position.x, 0, Time.deltaTime);
        position.z = _playerRigid.position.z + _boostSpeed * Time.deltaTime;
        _playerRigid.MovePosition(position);
    }
}
