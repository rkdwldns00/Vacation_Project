using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    public static void HandleCoroutine(IEnumerator coroutine)
    {
        CoroutineHandler handler = new GameObject("CoroutineHandler").AddComponent<CoroutineHandler>();
        handler.StartCoroutine(coroutine);
    }
}
