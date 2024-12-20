using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 코드 작성자 : 강지운 */
public enum EUIType
{
    Page,
    Popup
}

public abstract class ManagedUI : MonoBehaviour
{
    public event Action OnOpenAction;
    public event Action OnCloseAction;

    public bool IsOpen { get; private set; }

    public virtual void Awake()
    {
        UIManager.ManagerInstance.RegistUI(this);
    }

    public void OpenUI(EUIType uiOpenType = EUIType.Page)
    {
        UIManager.ManagerInstance.OpenUI(this, uiOpenType);
    }

    public void CloseUI()
    {
        UIManager.ManagerInstance.CloseUI(this);
    }

    protected abstract void OnClose();
    protected abstract void OnOpen();

    private void SetActive(bool active)
    {
        if (active)
        {
            if (!IsOpen)
            {
                OnOpen();
                OnOpenAction?.Invoke();

                IsOpen = true;
            }
        }
        else
        {
            if (IsOpen)
            {
                OnClose();
                OnCloseAction?.Invoke();
                
                IsOpen = false;
            }
        }
    }


    private class UIManager : MonoBehaviour
    {
        private static UIManager _managerInstance;
        public static UIManager ManagerInstance
        {
            get
            {
                if (_managerInstance == null)
                {
                    GameObject g = new GameObject("UIManager");
                    _managerInstance = g.AddComponent<UIManager>();
                }
                return _managerInstance;
            }
        }

        private List<ManagedUI> uiList = new();

        public void RegistUI(ManagedUI ui)
        {
            uiList.Add(ui);
        }

        public void OpenUI(ManagedUI ui, EUIType uiOpenType)
        {
            if (uiOpenType == EUIType.Page)
            {
                foreach (ManagedUI element in uiList)
                {
                    if (element != ui)
                    {
                        element.SetActive(false);
                    }
                }
            }

            ui.SetActive(true);
        }

        public void CloseUI(ManagedUI ui)
        {
            ui.SetActive(false);
        }
    }
}