using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 강지운 */
public class Chaser : MonoBehaviour
{
    [SerializeField] private ChaserData _chaserData;
    [SerializeField] private float _hitToPlayerTimer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _hitSpacingToPlayer;
    private float _curHitTimeDecreaseScore;

    public float HitToPlayerTimer => _hitToPlayerTimer;
    public float Timer { get; set; }

    private void Awake()
    {
        PlayerSpawner.Instance.OnSpawned += (_) => Timer = _hitToPlayerTimer;

        _curHitTimeDecreaseScore = _chaserData.hitTimeDecreaseScore;

        Timer = _hitToPlayerTimer;
    }

    private void Start()
    {
        transform.position = new Vector3(0, 0, -_hitToPlayerTimer * (_moveSpeed) - _hitSpacingToPlayer);
    }

    private void Update()
    {
        if (Player.Instance != null)
        {
            if (Timer > 0)
            {
                Vector3 p = Vector3.zero;

                p.z = Player.Instance.transform.position.z - Timer * (_moveSpeed - Player.Instance.OriginMoveSpeed) - _hitSpacingToPlayer;
                p.z = Mathf.Max(transform.position.z, p.z);

                if (Timer < _hitToPlayerTimer / 2)
                {
                    p.x = Mathf.Lerp(transform.position.x, Player.Instance.transform.position.x, Time.deltaTime * 5);
                }
                else
                {
                    p.x = transform.position.x;
                }

                transform.position = p;

                Timer = Mathf.Max(0, Timer - Time.deltaTime);
            }
            else if (Timer == 0)
            {
                HitToPlayer();
            }
        }
        else
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }

        DecreaseHitToPlayerTime();
    }

    public void HitToPlayer()
    {
        if (Player.Instance.BuffSystem.ContainBuff<ResurrectionBuff>())
        {
            return;
        }

        if (Player.Instance.UseBoost())
        {
            Timer = _hitToPlayerTimer;
        }
        else
        {
            Player.Instance.Kill();
        }
    }

    private void DecreaseHitToPlayerTime()
    {
        if (GameManager.Instance.Score >= _curHitTimeDecreaseScore && _curHitTimeDecreaseScore <= _chaserData.hitTimeDecreaseScore * _chaserData.hitTimeDecreaseCount)
        {
            _hitToPlayerTimer = Mathf.Max(0, _hitToPlayerTimer - _chaserData.hitTimeDecreaseValue);
            _curHitTimeDecreaseScore += _chaserData.hitTimeDecreaseScore;
        }
    }

    private void ResetTimer()
    {
        Timer = _hitToPlayerTimer;
    }
}
