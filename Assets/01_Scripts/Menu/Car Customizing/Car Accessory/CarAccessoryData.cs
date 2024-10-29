using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Car Accessory Data", menuName = "Car Customizing Datas/Car Accessory Data")]
public class CarAccessoryData : ScriptableObject
{
    public string Name;
    public GameObject AccessoryObjectPrefab;
    public CarAccessoryPositionType AccessoryPositionType;
    public Sprite AccessoryImage;

    [Header("Cost")]
    public int UnlockGoldCost;
    public int UnlockCrystalCost;
}

public enum CarAccessoryPositionType
{
    Top,
    Back
}