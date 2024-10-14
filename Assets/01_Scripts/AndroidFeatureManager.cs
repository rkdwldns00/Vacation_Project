using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidFeatureManager : MonoBehaviour
{
    private static AndroidFeatureManager _instance;

    public static AndroidFeatureManager Instance
    {
        get
        {
            if (_instance is null)
            {
                GameObject g = new GameObject("AndroidFeatureManager");
                _instance = g.AddComponent<AndroidFeatureManager>();
                DontDestroyOnLoad(g);
            }
            return _instance;
        }
    }

    private AndroidJavaClass _javaClass = null;
    private AndroidJavaObject _javaClassInstance = null;

    private void Awake()
    {
        _javaClass = new AndroidJavaClass("com.example.plugin.Plugin");
        if (_javaClass is not null)
        {
            _javaClassInstance = _javaClass.CallStatic<AndroidJavaObject>("GetInstance");
        }
    }

    public void ShowToast(string massage)
    {
        _javaClassInstance.Call("ShowToast", massage);
    }

    public void Share(string text)
    {
        _javaClassInstance.Call("Share", text, "");
    }
}
