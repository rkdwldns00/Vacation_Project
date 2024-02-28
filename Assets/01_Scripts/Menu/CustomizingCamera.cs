using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizingCamera : MonoBehaviour
{
    private const float MODEL_SPACING = 5f;

    [SerializeField] private GameObject[] modelPrefabs;
    [SerializeField] private Transform prefabSliderTransform;
    [SerializeField] private float startRot;
    [SerializeField] private float rotateSpeed;

    private GameObject[] models;
    private int showedCarIndex;

    private void Start()
    {
        models = new GameObject[modelPrefabs.Length];
        for (int i = 0; i < modelPrefabs.Length; i++)
        {
            GameObject g = Instantiate(modelPrefabs[i], prefabSliderTransform);
            g.transform.localPosition = new Vector3(i * MODEL_SPACING, 0, 0);
            g.transform.rotation = Quaternion.Euler(0, startRot, 0);

            models[i] = g;
        }

        showedCarIndex = GameManager.Instance.PlayerModelId;
        prefabSliderTransform.localPosition = new Vector3(-showedCarIndex * MODEL_SPACING, 0, 0);
    }

    private void Update()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].transform.rotation *= Quaternion.Euler(0, rotateSpeed * Time.deltaTime, 0);
        }
        prefabSliderTransform.localPosition = Vector3.Lerp(prefabSliderTransform.localPosition, new Vector3(-showedCarIndex * MODEL_SPACING, 0, 0), Time.deltaTime * 3);
    }

    public void ShowCar(int index)
    {
        showedCarIndex = index;
    }
}
