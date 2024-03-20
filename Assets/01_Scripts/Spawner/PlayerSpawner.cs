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
    public GameObject[] PlayerModels
    {
        get
        {
            GameObject[] meshes = new GameObject[PlayerModels.Length];
            for(int i=0; i<meshes.Length; i++)
            {
                meshes[i] = _playerModelPrefabs[i].GetComponentInChildren<Player>().playerMesh;
            }
            return meshes;
        }
    }

    [SerializeField] private GameObject[] _playerModelPrefabs;


    private void Start()
    {
        SpawnHandler();
    }

    private void SpawnHandler()
    {
        GameObject g = Instantiate(_playerModelPrefabs[GameManager.Instance.PlayerModelId], transform.position, transform.rotation);
        Player player = g.GetComponent<Player>();

        OnSpawned?.Invoke(player);
    }
}
