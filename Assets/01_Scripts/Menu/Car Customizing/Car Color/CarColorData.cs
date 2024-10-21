using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Color Data", menuName = "Car Customizing Datas/Car Color Data")]
public class CarColorData : ScriptableObject
{
    public string Name;
    public Material ColorMaterial;

    [Header("Price")]
    public int UnlockGoldCost;
    public int UnlockCrystalCost;

    
}
