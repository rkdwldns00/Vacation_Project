using System;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event Action OnDie;
    public event Action OnDamaged;
    public event Action OnHealed;
    public event Action OnChangedHealth;
    public event Action<float> OnChangedBoostGazy;

    public virtual int MaxLevel => 1;
    public virtual bool IsUpgradable => PlayerLevel < MaxLevel;
    public virtual int UpgradeCost => PlayerLevel * UnlockCost * 10;
    public virtual int UnlockCost => 0;
    public virtual string CarInfo => "기본 차량";

    public int CurruntHealth { get; set; }

    private float _moveSpeed;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    public float OriginMoveSpeed => _playerSetting._moveSpeed;

    private float _boostGazy;
    public float BoostGazy
    {
        get
        {
            return _boostGazy;
        }
        protected set
        {
            _boostGazy = Mathf.Clamp(value, 0, _playerSetting._maxBoostGazy);
        }
    }

    public int PlayerLevel
    {
        get => PlayerPrefs.GetInt("Player" + CarID + "Level");
        set => PlayerPrefs.SetInt("Player" + CarID + "Level", value);
    }

    public bool CanControl { get; set; } = true;
    public GameObject playerMesh;
    private Mesh _mesh;
    [SerializeField] private int CarID;
    [SerializeField] private PlayerSetting _playerSetting;

    public virtual int MaxHealth
    {
        get => _playerSetting._maxHealth;
    }

    public virtual float MaxBoostGazy
    {
        get => _playerSetting._maxBoostGazy;
    }

    protected Rigidbody _rigid;
    private float _targetX;
    private float _colliderBoundMinY;
    private bool _isDead;
    private bool _isDebuggingMode;

    protected virtual void Awake()
    {
        Instance = this;
        _rigid = GetComponent<Rigidbody>();

        CurruntHealth = _playerSetting._maxHealth;
        OnChangedHealth?.Invoke();

        BoxCollider meshCollder = GetComponentInChildren<BoxCollider>();
        float meshMinY = meshCollder.center.y - meshCollder.size.y / 2;
        _colliderBoundMinY = meshMinY;

        _mesh = playerMesh.GetComponentInChildren<MeshFilter>().sharedMesh;
        _moveSpeed = _playerSetting._moveSpeed;
    }

    protected virtual void Start()
    {
        SetGoldRate();
        OnChangedBoostGazy?.Invoke(0);
        GameManager.Instance.GemScore = 0;
    }

    protected virtual void Update()
    {
        UpdaateDistanceScore();
        MoveHandler();
    }

    protected virtual void FixedUpdate()
    {
        Vector3 rotation = Vector3.zero;
        Vector3 position = Vector3.zero;

        position.z = _rigid.position.z + MoveSpeed * Time.fixedDeltaTime;

        position.x = Mathf.Lerp(_rigid.position.x, _targetX, Time.fixedDeltaTime * 10);
        rotation.y = _rigid.position.x - position.x;

        position.y = _rigid.position.y;

        rotation.x = -_rigid.velocity.y;

        _rigid.rotation = Quaternion.Euler(rotation);
        _rigid.MovePosition(position);

        if (position.y < -10)
        {
            DieHandler();
        }
        else if (position.y < _playerSetting.fallingSensorY)
        {
            if (_rigid.SweepTest(Vector3.forward, out _, 0.1f) || _rigid.SweepTest(Vector3.right, out _, 0.1f) || _rigid.SweepTest(Vector3.left, out _, 0.1f))
            {
                DieHandler();
            }
        }
    }

    protected virtual void Reset()
    {
        MeshFilter meshFilter = GetComponentInChildren<MeshFilter>();

        if (meshFilter != null)
        {
            playerMesh = meshFilter.gameObject;
        }
    }

    protected virtual void SetGoldRate()
    {
        GameManager.Instance.RewardGoldRate = 0.1f;
    }

    protected virtual void UpdaateDistanceScore()
    {
        GameManager.Instance.DistanceScore = (int)transform.position.z;
    }

    protected virtual void MoveHandler()
    {
        if (!CanControl) return;

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
        _targetX = Mathf.Lerp(_playerSetting.minX, _playerSetting.maxX, xRate);
    }

    public virtual void Jump()
    {
        Vector3 rayOrigin = new Vector3(_rigid.position.x,
            _rigid.position.y + _colliderBoundMinY + 0.05f,
            _rigid.position.z);
        if (Physics.Raycast(rayOrigin, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("Road")))
        {
            _rigid.velocity = (Vector3.up * _playerSetting._jumpPower);
            Instantiate(_playerSetting.playerJumpEffect, transform.position, Quaternion.identity);
        }
    }

    public virtual void ChargeBoost(float value)
    {
        BoostGazy += value;
        GameManager.Instance.GemScore += 10;
        OnChangedBoostGazy?.Invoke(value);
    }

    public virtual bool UseBoost()
    {
        if (BoostGazy > 1)
        {
            BoostGazy -= 1;
            OnChangedBoostGazy?.Invoke(-1);
            return true;
        }
        return false;
    }

    [ContextMenu("StartDebuggingMode")]
    public void StartDubuggingMode()
    {
        _isDebuggingMode = true;
    }

    protected void RunOnChargeBoostGazy(float value)
    {
        OnChangedBoostGazy?.Invoke(value);
    }

    protected void RunOnChangedBoostGazy(float value)
    {
        OnChangedBoostGazy?.Invoke(value);
    }

    #region 체력 관리

    public virtual void TakeDamage(int damage = 1)
    {
        if (GetComponent<BuffSystem>().ContainBuff<ObstacleShieldBuff>()) return;

        CurruntHealth -= damage;

        OnDamaged?.Invoke();
        OnChangedHealth?.Invoke();

        Instantiate(_playerSetting.playerDamagedEffect, transform.position, Quaternion.identity);

        if (CurruntHealth <= 0)
        {
            DieHandler();
        }
    }

    public void TakeHeal(int heal = 1)
    {
        if (CurruntHealth == MaxHealth || _isDead) return;

        CurruntHealth = Mathf.Min(CurruntHealth + heal, MaxHealth);

        OnHealed?.Invoke();
        OnChangedHealth?.Invoke();
    }

    public void Kill()
    {
        DieHandler();
    }

    private void DieHandler()
    {
        if (_isDead) return;
#if UNITY_EDITOR
        if (_isDebuggingMode) return;
#endif
        _isDead = true;

        GameManager.Instance.isBestScore = GameManager.Instance.Score > GameManager.Instance.BestScore;
        if (GameManager.Instance.isBestScore)
        {
            GameManager.Instance.BestScore = GameManager.Instance.Score;
        }
        if (Currency.Ticket > 0)
        {
            Currency.Ticket--;
            GameManager.Instance.RewardGoldAdded = (int)(GameManager.Instance.RewardGold * 0.5f);
            GameManager.Instance.RewardCrystalAdded = 1;
        }
        else
        {
            GameManager.Instance.RewardGoldAdded = 0;
            GameManager.Instance.RewardCrystalAdded = 0;
        }
        Currency.Gold += GameManager.Instance.RewardGold;
        Currency.Crystal += GameManager.Instance.RewardCrystal;
        InGameUIManager.Instance.ActiveGameResultUI();

        OnDie?.Invoke();

        Material[] materials = playerMesh.GetComponentInChildren<MeshRenderer>().materials;
        Vector3 scale = playerMesh.GetComponentInChildren<MeshRenderer>().transform.localScale;
        float yPos = playerMesh.transform.localPosition.y;
        PlayerDeadObject obj = Instantiate(_playerSetting.playerDeadObject, transform.position, Quaternion.identity).GetComponent<PlayerDeadObject>();
        obj.SetDeadObjectData(_mesh, materials, MoveSpeed, scale, yPos);

        Destroy(gameObject);
    }
    #endregion
}
