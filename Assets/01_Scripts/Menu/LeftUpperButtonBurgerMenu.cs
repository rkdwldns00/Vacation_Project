using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftUpperButtonBurgerMenu : MonoBehaviour
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _activeButton;

    private void Awake()
    {
        _activeButton.onClick.AddListener(() => _layer.SetActive(!_layer.activeSelf));
    }
}
