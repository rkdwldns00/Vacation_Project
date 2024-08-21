using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoostBuff : Buff
{
    private Rigidbody _playerRigid;
    private float _boostSpeed;
    private float _durationTime;
    private Player _player;

    public BoostBuff(float boostSpeed, float durationTime)
    {
        _boostSpeed = boostSpeed;
        _durationTime = durationTime;
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        _playerRigid = buffSystem.GetComponent<Rigidbody>();
        _player = buffSystem.GetComponent<Player>();
        _player.AddMoveSpeed(_boostSpeed);
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0) buffSystem.RemoveBuff(this);
    }

    public override void EndBuff(BuffSystem buffSystem)
    {
        _player.AddMoveSpeed(_boostSpeed);
    }
}
