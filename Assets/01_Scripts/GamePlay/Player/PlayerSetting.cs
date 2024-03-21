using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "플레이어 설정", menuName = "Player Setting", order = int.MinValue)]
public class PlayerSetting : ScriptableObject
{
    [Header("이동 설정")]
    public float _minX;
    public float _maxX;
    public float _fallingSensorY;
    [Header("에셋 관리")]
    public GameObject[] _playerPrefabs;

    private static GameObject[] _playerModels = null;
    public GameObject[] PlayerModels
    {
        get
        {
            if (_playerModels == null)
            {
                _playerModels = new GameObject[_playerPrefabs.Length];
                for (int i = 0; i < _playerModels.Length; i++)
                {
                    _playerModels[i] = _playerPrefabs[i].GetComponentInChildren<Player>().playerMesh;
                }
            }
            return _playerModels;
        }
    }
}
