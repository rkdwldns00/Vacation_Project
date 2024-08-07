using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuitUI : MonoBehaviour
{
    [SerializeField] private GameObject _gameQuitUILayer;
    [SerializeField] private Button _gameQuitUIActiveButton;
    [SerializeField] private Button _gameQuitButton;
    [SerializeField] private Button _gameQuitCancelButton;

    private void Awake()
    {
        _gameQuitUIActiveButton.onClick.AddListener(() => _gameQuitUILayer.SetActive(true));
        _gameQuitButton.onClick.AddListener(() => Application.Quit());
        _gameQuitCancelButton.onClick.AddListener(() => _gameQuitUILayer.SetActive(false));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameQuitUILayer.SetActive(true);
        }
    }
}
