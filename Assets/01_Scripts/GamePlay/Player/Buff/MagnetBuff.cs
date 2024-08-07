using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBuff : Buff
{
    private Player player;
    private float _magnetStrength;
    private float _durationTime;

    public MagnetBuff(float magnetStrength, float durationTime)
    {
        _magnetStrength = magnetStrength;
        _durationTime = durationTime;
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        player = buffSystem.GetComponent<Player>();
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0) buffSystem.RemoveBuff(this);

        Collider[] gems = Physics.OverlapSphere(player.transform.position, _magnetStrength, LayerMask.GetMask("Gem"));

        foreach (Collider gem in gems)
        {
            Vector3 dir = player.transform.position - gem.transform.position;
            gem.transform.Translate(dir.normalized * player.MoveSpeed * 2 * Time.deltaTime);
        }
    }
}
