using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "플레이어 설정", menuName = "Player Setting", order = int.MinValue)]
public class PlayerSetting : ScriptableObject
{
    [Header("이동 설정")]
    public float minX;
    public float maxX;
    public float fallingSensorY;

    [Header("기본 스탯")]
    public float _moveSpeed;
    public float _jumpPower;
    public int _maxHealth;
    public float _maxBoostGazy;

    [Header("에셋 관리")]
    public GameObject[] playerPrefabs;
    public GameObject playerDamagedEffect;
    public GameObject playerJumpEffect;
    public GameObject playerDeadObject;
    public GameObject playerInvincibleShield;

    [Header("스피드 레벨스케일링")]
    public float maxSpeedMagnification;
    public int speedIncreaseScore;
    public int maxSpeedIncreaseCount;

    private static GameObject[] _playerModels = null;
    public GameObject[] PlayerModels
    {
        get
        {
            if (_playerModels == null)
            {
                _playerModels = new GameObject[playerPrefabs.Length];
                for (int i = 0; i < _playerModels.Length; i++)
                {
                    _playerModels[i] = playerPrefabs[i].GetComponentInChildren<Player>().playerMesh;
                }
            }
            return _playerModels;
        }
    }
}
