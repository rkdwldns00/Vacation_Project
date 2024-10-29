using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 자동차별 악세서리 위치 설정자 클래스
public class CarAccessoryPositioner : MonoBehaviour
{
    [SerializeField] private GameObject _accessoryObject;
    [SerializeField] private Transform _topAccessoryPosition;
    [SerializeField] private Transform _backAccessoryPosition;

    public void SetCarAccessoryObject(CarAccessoryData carAccessoryData)
    {
        Vector3 carAccessoryPosition = _topAccessoryPosition.position;
        Vector3 carAccessoryScale = carAccessoryData ? carAccessoryData.AccessorySize : Vector3.one;
        Mesh mesh = null;
        Material[] materials = new Material[1];

        switch (carAccessoryData?.AccessoryPositionType)
        {
            case CarAccessoryPositionType.Top:
                carAccessoryPosition = _topAccessoryPosition.position;
                break;
            case CarAccessoryPositionType.Back:
                carAccessoryPosition = _backAccessoryPosition.position;
                break;
        }

        if (carAccessoryData != null)
        {
            mesh = carAccessoryData.AccessoryObjectPrefab.GetComponent<MeshFilter>().sharedMesh;
            materials = carAccessoryData.AccessoryObjectPrefab.GetComponent<MeshRenderer>().sharedMaterials;
        }

        _accessoryObject.transform.position = carAccessoryPosition;
        _accessoryObject.transform.localScale = carAccessoryScale;
        _accessoryObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        _accessoryObject.GetComponent<MeshRenderer>().sharedMaterials = materials;
    }
}
