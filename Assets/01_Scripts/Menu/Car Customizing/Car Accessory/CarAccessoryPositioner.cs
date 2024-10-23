using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 자동차별 악세서리 위치 설정자 클래스
public class CarAccessoryPositioner : MonoBehaviour
{
    [SerializeField] private GameObject _instantiatedAccessoryObject;
    private Transform _topAccessoryPosition;
    private Transform _backAccessoryPosition;

    public void InstantiateCarAccessory(GameObject accessoryObjectPrefab, CarAccessoryPositionType carAccessoryPositionType)
    {
        Transform carAccessoryPosition = null;

        switch (carAccessoryPositionType)
        {
            case CarAccessoryPositionType.Top:
                carAccessoryPosition = _topAccessoryPosition;
                break;
            case CarAccessoryPositionType.Back:
                carAccessoryPosition = _backAccessoryPosition;
                break;
        }

        _instantiatedAccessoryObject = Instantiate(accessoryObjectPrefab, carAccessoryPosition);
    }
}
