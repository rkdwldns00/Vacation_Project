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
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayerSpawner>();
            }
            return _instance;
        }
    }

    public event Action<Player> OnSpawned;

    [SerializeField] private PlayerSetting _playerSetting;


    private void Start()
    {
        SpawnHandler();
    }

    private void SpawnHandler()
    {
        GameObject g = Instantiate(_playerSetting.playerPrefabs[GameManager.Instance.PlayerModelId], transform.position, transform.rotation);
        Player player = g.GetComponent<Player>();

        OnSpawned?.Invoke(player);
    }
}
