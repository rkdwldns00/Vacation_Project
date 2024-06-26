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
    [Header("에셋 관리")]
    public GameObject[] playerPrefabs;

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
