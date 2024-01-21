using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class Player : MonoBehaviour
{
    public int CurruntHealth { get; set; }
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
    }

    private float _boostGazy;
    public float boostGazy
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
    private float _VelocityY;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        CurruntHealth = _MaxHealth;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.zero;
        Vector3 position = Vector3.zero;

        position.z = _rigid.position.z + _moveSpeed * Time.fixedDeltaTime;

        position.x = Mathf.Lerp(_targetX, _rigid.position.x, Time.fixedDeltaTime);
        rotation.y = _rigid.position.x - position.x;

        position.y = Mathf.Max(0, _rigid.position.y + _VelocityY * Time.fixedDeltaTime);
        if (position.y > 0)
        {
            float yDelta = 9.8f * Time.fixedDeltaTime;

            rotation.x = -yDelta;

            _VelocityY -= 9.8f * Time.fixedDeltaTime;
        }
        else
        {
            _VelocityY = 0;
        }

        _rigid.rotation = Quaternion.Euler(rotation);
        _rigid.MovePosition(position);
        if(position.y < _fallingSensorY)
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
            _VelocityY = +_jumpPower;
        }
    }

    public void ChargeBoost(float value)
    {
        boostGazy += value;
    }

    public bool UseBoost()
    {
        if (boostGazy > 1)
        {
            boostGazy -= 1;
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
        Destroy(gameObject);
    }
    #endregion
}
