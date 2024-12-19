using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoostManager : MonoBehaviour
{
    public static float BoostDistance;
    public static int BoostCost;

    [SerializeField] private float _boostSpeed;

    private void Awake()
    {
        PlayerSpawner.Instance.OnSpawned += OnAddBoost;
    }

    private void OnAddBoost(Player player)
    {
        if (BoostDistance > 0)
        {
            Currency.Gold -= BoostCost;

            BuffSystem buffSystem = player.GetComponent<BuffSystem>();

            buffSystem.AddBuff(new StartBoostBuff(_boostSpeed, BoostDistance));

            BoostDistance = 0;
            BoostCost = 0;
        }
    }
}
