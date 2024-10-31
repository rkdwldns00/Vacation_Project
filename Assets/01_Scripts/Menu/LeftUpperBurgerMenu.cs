using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftUpperBurgerMenu : MonoBehaviour
{
    [SerializeField] private GameObject _layer;
    [SerializeField] private Button _burgerMenuActiveBtn;

    private void Awake()
    {
        _burgerMenuActiveBtn.onClick.AddListener(() => _layer.SetActive(!_layer.activeSelf));
    }
}
