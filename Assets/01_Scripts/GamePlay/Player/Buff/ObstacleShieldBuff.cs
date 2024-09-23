using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShieldBuff : Buff
{
    private GameObject _shieldEffectPrefab;
    private GameObject _instantiatedShieldEffect;
    private Material _instantiatedShieldMaterial;
    private Color _originShieldColor;
    private bool _isFadeInColor;
    private float _durationTime;

    public ObstacleShieldBuff(GameObject shieldEffectPrefab, float durationTime)
    {
        _shieldEffectPrefab = shieldEffectPrefab;
        _durationTime = durationTime;
    }

    public override void MergeBuff<T>(T otherBuff)
    {
        ObstacleShieldBuff mergedBuff = otherBuff as ObstacleShieldBuff;

        if (mergedBuff._durationTime > _durationTime)
        {
            _durationTime = mergedBuff._durationTime;
        }
    }

    public override void StartBuff(BuffSystem buffSystem)
    {
        if (_shieldEffectPrefab != null) _instantiatedShieldEffect = GameObject.Instantiate(_shieldEffectPrefab, buffSystem.transform);
        if (_instantiatedShieldEffect != null)
        {
            _instantiatedShieldEffect.transform.localPosition = Vector3.zero;
            _instantiatedShieldMaterial = _instantiatedShieldEffect.GetComponent<MeshRenderer>().material;
            _originShieldColor = _instantiatedShieldMaterial.color;
        }
    }

    public override void UpdateBuff(BuffSystem buffSystem)
    {
        _durationTime -= Time.deltaTime;

        if (_durationTime <= 0)
        {
            if (_instantiatedShieldEffect != null) GameObject.Destroy(_instantiatedShieldEffect);
            buffSystem.RemoveBuff(this);
        }
        else if (_durationTime < 2f)
        {
            if (_instantiatedShieldEffect != null)
            {
                if (_isFadeInColor)
                {
                    _instantiatedShieldMaterial.color = _instantiatedShieldMaterial.color - new Color(0, 0, 0, Time.deltaTime * (5 - _durationTime));
                    if (_instantiatedShieldMaterial.color.a <= 0.1f) _isFadeInColor = false;
                }
                else
                {
                    _instantiatedShieldMaterial.color = _instantiatedShieldMaterial.color + new Color(0, 0, 0, Time.deltaTime * (5f - _durationTime));
                    if (_instantiatedShieldMaterial.color.a >= _originShieldColor.a) _isFadeInColor = true;
                }
            }
        }
        else if (_instantiatedShieldEffect != null)
        {
            _instantiatedShieldMaterial.color = _originShieldColor;
        }
    }

    public override void EndBuff(BuffSystem buffSystem)
    {

    }
}
