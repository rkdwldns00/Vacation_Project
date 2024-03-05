using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event Action OnDie;
    public event Action OnDamaged;
    public event Action<float> OnChangedBoostGazy;

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
    [SerializeField] private int _maxHealth;

    public int MaxHealth
    {
        get => _maxHealth;
    }

    protected Rigidbody _rigid;
    private float _targetX;
    private float _colliderBoundMinY;
    private bool _isDead;

    protected virtual void Awake()
    {
        Instance = this;
        _rigid = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        CurruntHealth = _maxHealth;

        BoxCollider meshCollder = GetComponentInChildren<BoxCollider>();
        float meshMinY = meshCollder.center.y - meshCollder.size.y / 2;
        _colliderBoundMinY = meshMinY;
    }

    protected virtual void Update()
    {
        GameManager.Instance.Score = (int)transform.position.z;
        MoveHandler();
    }

    protected virtual void FixedUpdate()
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

    protected virtual void MoveHandler()
    {
        if (Input.GetMouseButton(0))
        {
            float xRate = Input.mousePosition.x / Screen.width;
            Move(xRate);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Jump();
        }
    }

    public virtual void Move(float xRate)
    {
        _targetX = Mathf.Lerp(_minX, _maxX, xRate);
    }

    public virtual void Jump()
    {
        Vector3 rayOrigin = new Vector3(_rigid.position.x,
            _rigid.position.y + _colliderBoundMinY + 0.05f,
            _rigid.position.z);
        if (Physics.Raycast(rayOrigin, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("Road")))
        {
            _rigid.velocity = (Vector3.up * _jumpPower);
        }
    }

    public virtual void ChargeBoost(float value)
    {
        BoostGazy += value;
        OnChangedBoostGazy?.Invoke(value);
    }

    public bool UseBoost()
    {
        if (BoostGazy > 1)
        {
            BoostGazy -= 1;
            OnChangedBoostGazy?.Invoke(-1);
            return true;
        }
        return false;
    }

    #region 체력 관리

    public virtual void TakeDamage(int damage = 1)
    {
        CurruntHealth -= damage;

        OnDamaged?.Invoke();

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

        GameManager.Instance.isBestScore = GameManager.Instance.Score > GameManager.Instance.BestScore;
        if (GameManager.Instance.isBestScore)
        {
            GameManager.Instance.BestScore = GameManager.Instance.Score;
        }
        InGameUIManager.Instance.ActiveGameResultUI();

        OnDie?.Invoke();

        Destroy(gameObject);
    }
    #endregion
}
