using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/* 코드 작성자
 * 이동 설정, 기본 스탯, 모델 구현 : 강지운
 * 에셋 관리, 스피드 레벨스케일링 구현 : 이기환
 */
[CreateAssetMenu(fileName = "플레이어 설정", menuName = "Player Setting", order = int.MinValue)]
public class PlayerSetting : ScriptableObject
{
    [Header("이동 설정")]
    public float minX;
    public float maxX;
    public float fallingSensorY;

    [Header("기본 스탯")]
    public float moveSpeed;
    public float jumpPower;
    public int maxHealth;
    public float maxBoostGazy;

    [Header("에셋 관리")]
    public GameObject[] playerPrefabs;
    public GameObject playerDamagedEffect;
    public GameObject playerJumpEffect;
    public GameObject playerDeadObject;
    public GameObject playerInvincibleShield;

    [Header("스피드 레벨스케일링")]
    public float maxSpeedMagnification;
    public int speedIncreaseScore;
    public int maxSpeedIncreaseCount;

    private static GameObject[] playerModels = null;
    public GameObject[] PlayerModels
    {
        get
        {
            if (playerModels == null)
            {
                playerModels = new GameObject[playerPrefabs.Length];
                for (int i = 0; i < playerModels.Length; i++)
                {
                    playerModels[i] = playerPrefabs[i].GetComponentInChildren<Player>().playerMesh;
                }
            }
            return playerModels;
        }
    }
}
