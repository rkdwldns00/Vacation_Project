using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 자동차별 악세서리 위치 설정자 클래스
public class CarAccessoryPositioner : MonoBehaviour
{
    [SerializeField] private GameObject _accessoryObject;
    [SerializeField] private Transform _topAccessoryPosition;
    [SerializeField] private Transform _backAccessoryPosition;

    public void SetCarAccessoryObject(GameObject carAccessoryObject, CarAccessoryPositionType carAccessoryPositionType = CarAccessoryPositionType.Top)
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

        Mesh mesh = null;
        Material[] materials = new Material[1];

        if (carAccessoryObject != null)
        {
            mesh = carAccessoryObject.GetComponent<MeshFilter>().sharedMesh;
            materials = carAccessoryObject.GetComponent<MeshRenderer>().sharedMaterials;
        }

        _accessoryObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        _accessoryObject.GetComponent<MeshRenderer>().sharedMaterials = materials;
        _accessoryObject.transform.position = carAccessoryPosition.position;

    }
}
