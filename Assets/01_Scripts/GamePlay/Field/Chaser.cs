using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] private float _hitToPlayerTimer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _hitSpacingToPlayer;

    private float _timer;

    public float Timer
    {
        set => _timer = value;
    }

    private void Start()
    {
        _timer = _hitToPlayerTimer;

        transform.position = new Vector3(0, 0, -_hitToPlayerTimer * (_moveSpeed) - _hitSpacingToPlayer);
    }

    private void Update()
    {
        if (Player.Instance != null)
        {
            if (_timer > 0)
            {
                Vector3 p = Vector3.zero;

                p.z = Player.Instance.transform.position.z - _timer * (_moveSpeed - Player.Instance.MoveSpeed) - _hitSpacingToPlayer;
                p.z = Mathf.Max(transform.position.z, p.z);

                p.x = Mathf.Lerp(transform.position.x, Player.Instance.transform.position.x, Time.deltaTime * 5);

                transform.position = p;

                _timer = Mathf.Max(0, _timer - Time.deltaTime);
            }
            else if (_timer == 0)
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
            _timer = _hitToPlayerTimer;
        }
        else
        {
            Player.Instance.Kill();
        }
    }

    private void ResetTimer()
    {
        _timer = _hitToPlayerTimer;
    }
}
