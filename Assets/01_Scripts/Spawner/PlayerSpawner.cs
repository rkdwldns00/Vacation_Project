using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private static PlayerSpawner _instance;
    public static PlayerSpawner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerSpawner>();
            }
            return _instance;
        }
    }

    public event Action<Player> OnSpawned;

    [SerializeField] private PlayerSetting _playerSetting;
    private float _playerDieZ;


    private void Start()
    {
        GameManager.Instance.GemScore = 0;
        GameManager.Instance.DistanceScore = 0;

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject g = Instantiate(_playerSetting.playerPrefabs[GameManager.Instance.PlayerModelId], transform.position + transform.forward * _playerDieZ, transform.rotation);
        Player player = g.GetComponent<Player>();
        player.OnDie += () => _playerDieZ = player.transform.position.z;

        OnSpawned?.Invoke(player);
    }
}
