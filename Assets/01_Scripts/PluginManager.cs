using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PluginManager : MonoBehaviour
{
    private static PluginManager _instance;

    public static PluginManager Instance
    {
        get
        {
            if (_instance is null)
            {
                GameObject g = new GameObject("PluginManager");
                _instance = g.AddComponent<PluginManager>();
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
