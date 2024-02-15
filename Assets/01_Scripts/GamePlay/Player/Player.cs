using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event Action OnDie;

    public int CurruntHealth { get; set; }
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
    }

    private float _boostGazy;
    public float BoostGazy
    {
        get
        {
            return _boostGazy;
        }
        protected set
        {
            _boostGazy = Mathf.Clamp(value, 0, 3);
        }
    }

    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _fallingSensorY;
    [SerializeField] private int _MaxHealth;

    private Rigidbody _rigid;
    private float _targetX;
    private bool _isDead;

    private void Awake()
    {
        Instance = this;
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        CurruntHealth = _MaxHealth;
    }

    private void Update()
    {
        GameManager.Instance.Score = (int)transform.position.z;
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.zero;
        Vector3 position = Vector3.zero;

        position.z = _rigid.position.z + _moveSpeed * Time.fixedDeltaTime;

        position.x = Mathf.Lerp(_rigid.position.x, _targetX, Time.fixedDeltaTime * 10);
        rotation.y = _rigid.position.x - position.x;

        position.y = _rigid.position.y;

        rotation.x = -_rigid.velocity.y;

        _rigid.rotation = Quaternion.Euler(rotation);
        _rigid.MovePosition(position);
        if (position.y < _fallingSensorY)
        {
            DieHandler();
        }
    }

    public void Move(float xRate)
    {
        _targetX = Mathf.Lerp(_minX, _maxX, xRate);
    }

    public void Jump()
    {
        if (_rigid.position.y < 0.1f)
        {
            _rigid.AddForce(Vector3.up * _jumpPower);
        }
    }

    public void ChargeBoost(float value)
    {
        BoostGazy += value;
    }

    public bool UseBoost()
    {
        if (BoostGazy > 1)
        {
            BoostGazy -= 1;
            return true;
        }
        return false;
    }

    #region 체력 관리

    public void TakeDamage(int damage = 1)
    {
        CurruntHealth -= damage;

        if (CurruntHealth <= 0)
        {
            DieHandler();
        }
    }

    public void Kill()
    {
        DieHandler();
    }

    private void DieHandler()
    {
        if (_isDead) return;
        _isDead = true;

        GameManager.Instance.isHighScore = GameManager.Instance.Score > GameManager.Instance.HighScore;
        if (GameManager.Instance.isHighScore)
        {
            GameManager.Instance.HighScore = GameManager.Instance.Score;
        }

        OnDie?.Invoke();
        Destroy(gameObject);
    }
    #endregion
}
