using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPatchNoteButton : MonoBehaviour
{
    [SerializeField] private PatchNoteUI _patchNoteUI;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClickButton);
    }

    public void OnClickButton()
    {
        _patchNoteUI.OpenUI();
    }
}
