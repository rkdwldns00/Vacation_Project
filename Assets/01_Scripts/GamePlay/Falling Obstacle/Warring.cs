using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warring : MonoBehaviour
{
    public void SetTime(float time)
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}
