using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginRewardBoxButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    
    public Button Button => _button;
    public Image Image => _image;
}
