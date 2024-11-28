using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBuff : Buff
{
    private Player player;
    public float MagnetStrength { get; set; }
    private float _durationTime;
    private bool _isDurationInfinity;

    public MagnetBuff(float magnetStrength, float durationTime)
    {
        MagnetStrength = magnetStrength;
        _durationTime = durationTime;
    }

    public MagnetBuff(float magnetStrength)
    {
        MagnetStrength = magnetStrength;
        _isDurationInfinity = true;
    }

    public override void MergeBuff<T>(T otherBuff)
    {
        MagnetBuff mergedBuff = otherBuff as MagnetBuff;

        if (mergedBuff._isDurationInfinity || _isDurationInfinity)
        {
            _isDurationInfinity = true;
        }
        else if (mergedBuff._durationTime > _durationTime)
        {
            _durationTime = mergedBuff._durationTime;
        }
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        player = buffSystem.GetComponent<Player>();
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0 && !_isDurationInfinity) buffSystem.RemoveBuff(this);

        Collider[] gems = Physics.OverlapSphere(player.transform.position, MagnetStrength, LayerMask.GetMask("Gem"));

        foreach (Collider gem in gems)
        {
            Vector3 dir = player.transform.position - gem.transform.position;
            gem.transform.Translate(dir.normalized * player.MoveSpeed * 2 * Time.deltaTime);
        }
    }

    public override void EndBuff(BuffSystem buffSystem)
    {

    }
}
