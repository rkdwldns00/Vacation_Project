using UnityEngine;

public class BoostBuff : Buff
{
    private Rigidbody _playerRigid;
    private Player _player;
    public float BoostSpeed { get; private set; }
    public float DurationTime { get; private set; }

    public BoostBuff(float boostSpeed, float durationTime)
    {
        BoostSpeed = boostSpeed;
        DurationTime = durationTime;
    }

    public override void MergeBuff<T>(T otherBuff)
    {
        BoostBuff mergedBuff = otherBuff as BoostBuff;

        if (mergedBuff.DurationTime > DurationTime)
        {
            DurationTime = mergedBuff.DurationTime;
        }
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        _playerRigid = buffSystem.GetComponent<Rigidbody>();
        _player = buffSystem.GetComponent<Player>();
        _player.AddMoveSpeed(BoostSpeed);
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        DurationTime -= Time.deltaTime;

        if (DurationTime <= 0) buffSystem.RemoveBuff(this);
    }

    public override void EndBuff(BuffSystem buffSystem)
    {
        _player.AddMoveSpeed(-BoostSpeed);
    }

    public void ChangeBoostSpeed(float speed)
    {
        float originSpeed = BoostSpeed;
        BoostSpeed = speed;

        _player.AddMoveSpeed(speed - originSpeed);
    }
}
