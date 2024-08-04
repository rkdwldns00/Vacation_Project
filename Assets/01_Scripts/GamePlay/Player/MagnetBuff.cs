using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBuff : MonoBehaviour
{
    private Player player;
    private float _magnetStrength;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Collider[] gems = Physics.OverlapSphere(transform.position, _magnetStrength, LayerMask.GetMask("Gem"));
        
        foreach (Collider gem in gems)
        {
            Vector3 dir = player.transform.position - gem.transform.position;
            gem.transform.Translate(dir.normalized * player.MoveSpeed * 2 * Time.deltaTime);
        }
    }

    public void AddMagnetStrength(float magnetStrength, float durationTime)
    {
        StartCoroutine(AddMagnetStrengthCoroutine(magnetStrength, durationTime));
    }

    private IEnumerator AddMagnetStrengthCoroutine(float magnetStrength, float durationTime)
    {
        _magnetStrength += magnetStrength;
        yield return new WaitForSeconds(durationTime);
        _magnetStrength -= magnetStrength;

        if (_magnetStrength <= 0)
        {
            Destroy(this);
        }

        yield break;
    }
}
