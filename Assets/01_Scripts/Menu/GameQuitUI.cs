using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameQuitUI : ManagedUI
{
    [SerializeField] private GameObject _gameQuitUILayer;
    [SerializeField] private Button _gameQuitUIActiveButton;
    [SerializeField] private Button _gameQuitButton;
    [SerializeField] private Button _gameQuitCancelButton;

    public override void Awake()
    {
        base.Awake();
        _gameQuitUIActiveButton.onClick.AddListener(() => OpenUI(EUIType.Page));
        _gameQuitButton.onClick.AddListener(QuitGame);
        _gameQuitCancelButton.onClick.AddListener(CloseUI);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameQuitUILayer.SetActive(true);
        }
    }

    protected override void OnOpen()
    {
        _gameQuitUILayer.SetActive(true);
    }

    protected override void OnClose()
    {
        _gameQuitUILayer.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
