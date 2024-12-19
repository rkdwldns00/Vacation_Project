using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*코드 작성자 : 강지운*/

public class PatchNoteUI : ManagedUI
{
    [SerializeField]private GameObject _layer;
    [SerializeField] private TextMeshProUGUI _versionText;
    [SerializeField] private Button _closeButton;


    public override void Awake()
    {
        base.Awake();
        RealtimeEventHandler.Instance.OnUpdate += (originVersion) => OpenUI(EUIType.Popup);
        _closeButton.onClick.AddListener(CloseUI);
    }

    private void Start()
    {
        _versionText.text = Application.version;
    }

    protected override void OnClose()
    {
        _layer.SetActive(false);
    }

    protected override void OnOpen()
    {
        _layer.SetActive(true);
    }
}
