using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBoostUIButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private BoostUI _boostUI;

    private void Awake()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _boostUI.OpenUI();
    }
}
