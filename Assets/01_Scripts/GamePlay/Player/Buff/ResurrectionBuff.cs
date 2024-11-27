using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResurrectionBuff : Buff
{
    private Player _player;
    private BuffSystem _buffSystem;

    public float ResurrectionGazy { get; private set; }
    public float DurationTime { get; private set; }
    public float RemainingTime { get; private set; }

    public ResurrectionBuff(float durationTime)
    {
        DurationTime = durationTime;
        RemainingTime = durationTime;
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        _player = buffSystem.GetComponent<Player>();
        _buffSystem = buffSystem;

        _player.BoostGazy = 0;
        _player.OnChangedBoostGazy += OnChargeBoost;
        _player.TakeDamage(2);
    }

    public override void MergeBuff<T>(T otherBuff)
    {

    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        RemainingTime -= Time.deltaTime;
        if (RemainingTime <= 0)
        {
            BoostPlayer boostPlayer = _player as BoostPlayer;
            if (boostPlayer != null)
            {
                boostPlayer.ResurrectionFail();
            }
            else
            {
                _player.Kill();
            }
        }
    }

    public override void EndBuff(BuffSystem buffSystem)
    {
        _player.OnChangedBoostGazy -= OnChargeBoost;
    }

    private void OnChargeBoost(float chargeValue)
    {
        ResurrectionGazy += chargeValue;

        if (ResurrectionGazy >= 1f)
        {
            _buffSystem.RemoveBuff(this);
            _player.TakeHeal(2);
            _player.ChargeBoost(2);
        }
    }
}
