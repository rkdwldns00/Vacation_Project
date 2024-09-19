using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private float _hitToPlayerTimer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _hitSpacingToPlayer;

    public float HitToPlayerTimer => _hitToPlayerTimer;
    public float Timer { get; set; }

    private void Awake()
    {
        PlayerSpawner.Instance.OnSpawned += (_) => Timer = _hitToPlayerTimer;
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
    }

    public void HitToPlayer()
    {
        if (Player.Instance.UseBoost())
        {
            Timer = _hitToPlayerTimer;
        }
        else
        {
            Player.Instance.Kill();
        }
    }

    private void ResetTimer()
    {
        Timer = _hitToPlayerTimer;
    }
}
