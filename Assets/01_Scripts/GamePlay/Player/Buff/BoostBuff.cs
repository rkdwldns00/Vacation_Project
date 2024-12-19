using UnityEngine;

public class BoostBuff : Buff
{
    protected Rigidbody playerRigid;
    protected Player player;
    protected BuffSystem buffSystem;
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
        this.buffSystem = buffSystem;

        playerRigid = this.buffSystem.GetComponent<Rigidbody>();
        player = this.buffSystem.GetComponent<Player>();
        player.AddMoveSpeed(BoostSpeed);
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        DurationTimeCheckHandler();
    }

    public override void EndBuff(BuffSystem buffSystem)
    {
        player.AddMoveSpeed(-BoostSpeed);
    }

    public void ChangeBoostSpeed(float speed)
    {
        float originSpeed = BoostSpeed;
        BoostSpeed = speed;

        player.AddMoveSpeed(speed - originSpeed);
    }

    protected virtual void DurationTimeCheckHandler()
    {
        DurationTime -= Time.deltaTime;

        if (DurationTime <= 0) buffSystem.RemoveBuff(this);
    }
}
