using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(CarAccessoryCustomizingUI))]
public class CarAccessoryImageGenerator : Editor
{
    private CarAccessoryCustomizingUI _carAccessoryCustomizingUI;

    private void OnEnable()
    {
        _carAccessoryCustomizingUI = (target as CarAccessoryCustomizingUI);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(25);

        if (GUILayout.Button("Generate Car Accessory Image"))
        {
            _carAccessoryCustomizingUI.GenerateCarAccessoryImage(GenerateCarAccessoryImageCoroutine());
        }

        GUILayout.Space(10);

        EditorGUILayout.HelpBox("자동차 액세서리 이미지 생성은 런타임에 실행하세요.\n" +
            "이미지 저장 공간 : " + Application.dataPath + "/05_Sprites/Car Accessory Image", MessageType.Info);
    }

    private IEnumerator GenerateCarAccessoryImageCoroutine()
    {
        foreach (CarAccessoryData data in _carAccessoryCustomizingUI.CarAccessoryDatas)
        {
            Texture2D carAccessoryTexture = null;

            while (carAccessoryTexture == null)
            {
                carAccessoryTexture = AssetPreview.GetAssetPreview(data.AccessoryObjectPrefab);
                yield return null;
            }

            byte[] bytes = ImageConversion.EncodeToPNG(carAccessoryTexture);
            File.WriteAllBytes(Application.dataPath + "/05_Sprites/Car Accessory Image/" + data.Name + ".png", bytes);
        }

        yield break;
    }
}
