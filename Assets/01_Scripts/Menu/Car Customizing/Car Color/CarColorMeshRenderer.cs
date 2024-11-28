using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColorMeshRenderer : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _carColorMeshRenderers;

    private void Start()
    {
        ChangeCarColor(CarColorCustomizingUI.EquippedColorMaterial);
    }

    public void ChangeCarColor(Material colorMaterial)
    {
        foreach (MeshRenderer renderer in _carColorMeshRenderers)
        {
            renderer.material = colorMaterial;
        }
    }
}
