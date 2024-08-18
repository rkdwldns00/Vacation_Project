using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShieldBuff : Buff
{
    private GameObject _shieldEffectPrefab;
    private GameObject _instantiatedShieldEffect;
    private float _durationTime;

    public ObstacleShieldBuff(GameObject shieldEffectPrefab, float durationTime)
    {
        _shieldEffectPrefab = shieldEffectPrefab;
        _durationTime = durationTime;
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        _instantiatedShieldEffect = GameObject.Instantiate(_shieldEffectPrefab, buffSystem.transform);
        _instantiatedShieldEffect.transform.localPosition = Vector3.zero;
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0)
        {
            GameObject.Destroy(_instantiatedShieldEffect);
            buffSystem.RemoveBuff(this);
        }
    }

    public override void EndBuff(BuffSystem buffSystem)
    {

    }
}
