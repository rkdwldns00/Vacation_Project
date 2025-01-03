using System;
using UnityEngine;
using UnityEngine.EventSystems;

/* 코드 작성자 : 강지운 */
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
    public virtual int UpgradeCost => PlayerLevel * UnlockCost;
    public virtual int UnlockCost => 0;
    public virtual string CarInfo => "기본 차량";
    public virtual float CarInfoFontSize => 50;

    public int CurruntHealth { get; set; }

    private float _moveSpeed;
    public virtual float MoveSpeed
    {
        get => _moveSpeed;
    }

    public float OriginMoveSpeed => _playerSetting.moveSpeed;
    public float MoveSpeedRate => MoveSpeed / OriginMoveSpeed;

    private float _boostGazy;
    public float BoostGazy
    {
        get
        {
            return _boostGazy;
        }
        set
        {
            float originBoostGazy = _boostGazy;
            _boostGazy = Mathf.Clamp(value, 0, MaxBoostGazy);

            OnChangedBoostGazy?.Invoke(_boostGazy - originBoostGazy);
        }
    }

    public int PlayerLevel
    {
        get => PlayerPrefs.GetInt("Player" + CarID + "Level");
        set => PlayerPrefs.SetInt("Player" + CarID + "Level", value);
    }

    public bool CanControl { get; set; } = true;
    public BuffSystem BuffSystem { get; set; }
    public GameObject playerMesh;
    private Mesh _mesh;
    [SerializeField] private int CarID;
    [SerializeField] private PlayerSetting _playerSetting;

    public virtual int MaxHealth
    {
        get => _playerSetting.maxHealth;
    }

    public virtual float MaxBoostGazy
    {
        get => _playerSetting.maxBoostGazy;
    }

    protected Rigidbody _rigid;
    private float _targetX;
    private float _colliderBoundMinY;
    private bool _isDead;
    private bool _isDebuggingMode;
    private float _beforeX;
    private int curSpeedIncreaseScore;

    protected virtual void Awake()
    {
        Instance = this;
        _rigid = GetComponent<Rigidbody>();
        BuffSystem = GetComponent<BuffSystem>();

        CurruntHealth = MaxHealth;
        OnChangedHealth?.Invoke();

        BoxCollider meshCollder = GetComponentInChildren<BoxCollider>();
        float meshMinY = meshCollder.center.y - meshCollder.size.y / 2;
        _colliderBoundMinY = meshMinY;

        _mesh = playerMesh.GetComponentInChildren<MeshFilter>().sharedMesh;
        _moveSpeed = _playerSetting.moveSpeed;

        curSpeedIncreaseScore = _playerSetting.speedIncreaseScore;
        OnDamaged += () => 
        {
            ObstacleShieldBuff obstacleShieldBuff = new ObstacleShieldBuff(_playerSetting.playerInvincibleShield, 1);
            GetComponent<BuffSystem>().AddBuff(obstacleShieldBuff);
        };
    }

    protected virtual void Start()
    {
        SetGoldRate();
        SetCrystalRate();
        OnChangedBoostGazy?.Invoke(0);
        SoundManager.Instance.PlaySound(SoundManager.Instance.SoundData.EngineSfx, SoundType.SFX, true);
    }

    protected virtual void Update()
    {
        UpdateDistanceScore();
        MoveHandler();
        IncreaseSpeedWithScore();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isDebuggingMode = true;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            _isDebuggingMode = false;
            Kill();
        }
#endif
    }

    protected virtual void FixedUpdate()
    {
        _beforeX = transform.position.x;
        Vector3 rotation = Vector3.zero;
        Vector3 position = Vector3.zero;

        position.z = _rigid.position.z + MoveSpeed * Time.fixedDeltaTime;

        position.x = Mathf.Lerp(_rigid.position.x, _targetX, Time.fixedDeltaTime * 10);
        rotation.y = (position.x - _rigid.position.x) * 25;

        position.y = _rigid.position.y;

        rotation.x = -_rigid.velocity.y;

        if (BuffSystem.ContainBuff<ObstacleShieldBuff>() && position.y < -0.1f)
        {
            position.y = -0.1f;
            _rigid.velocity = new Vector3(_rigid.velocity.x, 0, _rigid.velocity.z);
        }

        _rigid.rotation = Quaternion.Euler(rotation);
        _rigid.MovePosition(position);

        _rigid.AddForce(new Vector3(0, Physics.gravity.y * MoveSpeedRate * MoveSpeedRate - Physics.gravity.y, 0));

        if (position.y < -10)
        {
            DieHandler();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!BuffSystem.ContainBuff<ObstacleShieldBuff>() && transform.position.y < _playerSetting.fallingSensorY && collision.transform.tag == "Road")
        {
            DieHandler();
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

    protected virtual void SetCrystalRate()
    {
        GameManager.Instance.RewardCrystalRate = 0.01f;
    }

    private void IncreaseSpeedWithScore()
    {
        if (GameManager.Instance.Score >= curSpeedIncreaseScore && curSpeedIncreaseScore <= _playerSetting.speedIncreaseScore * _playerSetting.maxSpeedIncreaseCount)
        {
            AddMoveSpeed(_playerSetting.moveSpeed * (_playerSetting.maxSpeedMagnification - 1f) / _playerSetting.maxSpeedIncreaseCount);
            curSpeedIncreaseScore += _playerSetting.speedIncreaseScore;
        }
    }

    protected virtual void UpdateDistanceScore()
    {
        GameManager.Instance.DistanceScore = (int)transform.position.z;
    }

    protected virtual void MoveHandler()
    {
        if (!CanControl || EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButton(0))
        {
            float xRate = (Input.mousePosition.x / Screen.width - 0.5f) / Mathf.Lerp(0.8f, 0.3f, SettingUI.MoveSensitivity) + 0.5f;
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
        if (BuffSystem.ContainBuff<BoostBuff>())
        {
            return;
        }

        Vector3 rayOrigin = new Vector3(_rigid.position.x,
            _rigid.position.y + _colliderBoundMinY + 0.05f,
            _rigid.position.z);
        if (Physics.Raycast(rayOrigin, Vector3.down, 0.1f, 1 << LayerMask.NameToLayer("Road")))
        {
            _rigid.velocity = (Vector3.up * _playerSetting.jumpPower * MoveSpeedRate);
            Instantiate(_playerSetting.playerJumpEffect, transform.position, Quaternion.identity);
        }
    }

    public virtual void ChargeBoost(float value)
    {
        BoostGazy += value;
        GameManager.Instance.GemScore += 10;
    }

    public virtual bool UseBoost()
    {
        if (BuffSystem.ContainBuff<ResurrectionBuff>())
        {
            return false;
        }

        if (BoostGazy > 1)
        {
            BoostGazy -= 1;
            return true;
        }
        return false;
    }

    public void AddMoveSpeed(float speed)
    {
        _moveSpeed += speed;
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
        SoundManager.Instance.PlaySound(SoundManager.Instance.SoundData.PlayerDamagedSfx);

        if (CurruntHealth <= 0)
        {
            Kill();
        }
    }

    public virtual void TakeHeal(int heal = 1)
    {
        if (CurruntHealth == MaxHealth || _isDead) return;

        CurruntHealth = Mathf.Min(CurruntHealth + heal, MaxHealth);

        OnHealed?.Invoke();
        OnChangedHealth?.Invoke();
    }

    public virtual void Kill()
    {
        DieHandler();
    }

    public void DieHandler()
    {
        if (_isDead) return;
#if UNITY_EDITOR
        if (_isDebuggingMode) return;
#endif
        _isDead = true;

        InGameUIManager.Instance.ActiveGameResultUI();

        OnDie?.Invoke();

        Material[] materials = playerMesh.GetComponentInChildren<MeshRenderer>().materials;
        Vector3 scale = playerMesh.GetComponentInChildren<MeshRenderer>().transform.localScale;
        float yPos = playerMesh.transform.localPosition.y;
        PlayerDeadObject obj = Instantiate(_playerSetting.playerDeadObject, transform.position, Quaternion.identity).GetComponent<PlayerDeadObject>();
        Vector3 velocity = new Vector3((transform.position.x - _beforeX) * 10, GetComponent<Rigidbody>().velocity.y, MoveSpeed);
        obj.SetDeadObjectData(_mesh, materials, MoveSpeed, scale, yPos, velocity);

        Destroy(gameObject);
    }
    #endregion
}
