using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizingCamera : MonoBehaviour
{
    private const float MODEL_SPACING = 5f;

    [SerializeField] private Transform _prefabSliderTransform;
    [SerializeField] private float _startYRot;
    [SerializeField] private float _rotateSpeed;

    private GameObject[] _models;
    private int _showedCarIndex;
    private bool _isModelSpawned = false;

    public void ShowModels(GameObject[] modelPrefabs)
    {
        if (!_isModelSpawned)
        {
            _models = new GameObject[modelPrefabs.Length];
            for (int i = 0; i < modelPrefabs.Length; i++)
            {
                GameObject g = Instantiate(modelPrefabs[i], _prefabSliderTransform);
                g.transform.localPosition = new Vector3(i * MODEL_SPACING, 0, 0);
                g.transform.rotation = Quaternion.Euler(0, _startYRot, 0);

                _models[i] = g;
            }
            _isModelSpawned = true;
        }

        _showedCarIndex = GameManager.Instance.PlayerModelId;
        _prefabSliderTransform.localPosition = new Vector3(-_showedCarIndex * MODEL_SPACING, 0, 0);
    }

    private void Update()
    {
        if (_isModelSpawned)
        {
            for (int i = 0; i < _models.Length; i++)
            {
                _models[i].transform.rotation *= Quaternion.Euler(0, _rotateSpeed * Time.deltaTime, 0);
            }
            _prefabSliderTransform.localPosition = Vector3.Lerp(_prefabSliderTransform.localPosition, new Vector3(-_showedCarIndex * MODEL_SPACING, 0, 0), Time.deltaTime * 3);
        }
    }

    public void ShowCar(int index)
    {
        _showedCarIndex = index;
    }
}
