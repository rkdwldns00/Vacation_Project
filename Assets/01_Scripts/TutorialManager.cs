using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    public static bool isActive;

    public PlayerSetting playerSetting;
    public GameObject[] deleteObjects;
    public GameObject spikePrefab;
    public GameObject gemPrefab;

    public GameObject touchPlayerUI;
    public GameObject swipePlayerUI;
    public GameObject jumpPlayerUI;
    public GameObject hpUI;
    public GameObject gemUI;

    private bool playerHitted = false;
    private bool playerHittedByChaser = false;

    Player _player;

    private void Awake()
    {
        if (isActive)
        {
            PlayerSpawner.Instance.OnSpawned += (p) => { _player = p; };
            foreach (GameObject obj in deleteObjects)
            {
                obj.SetActive(false);
            }

        }
    }

    private void Start()
    {
        if (isActive)
        {
            StartCoroutine(TutorialRoutine());
        }
    }

    IEnumerator TutorialRoutine()
    {
        Chaser chaser = FindObjectOfType<Chaser>();
        chaser.Timer = 100;

        yield return new WaitForSeconds(1f);

        Time.timeScale = 0f;
        touchPlayerUI.SetActive(true);
#if UNITY_EDITOR
        yield return new WaitUntil(() => { return !Input.GetMouseButton(0); });
        yield return new WaitUntil(() => { return Input.GetMouseButton(0); });
#else
        yield return new WaitUntil(() => { return Input.touchCount == 0; });
        yield return new WaitUntil(() => { return Input.touchCount == 1; });
#endif
        touchPlayerUI.SetActive(false);
        Time.timeScale = 1f;

        yield return new WaitForSeconds(2f);

        Time.timeScale = 0f;
        swipePlayerUI.SetActive(true);
#if UNITY_EDITOR
        yield return new WaitUntil(() =>
        {
            return Input.GetMouseButton(0);
        });
#else
        yield return new WaitUntil(() =>
        {
            return Input.touchCount == 1 && Input.touches[0].deltaPosition.magnitude > 0;
        });
#endif
        swipePlayerUI.SetActive(false);
        Time.timeScale = 1f;

        yield return new WaitForSeconds(2f);

        Time.timeScale = 0f;
        jumpPlayerUI.SetActive(true);
#if UNITY_EDITOR
        yield return new WaitUntil(() =>
        {
            return Input.GetMouseButton(0);
        });
        yield return new WaitUntil(() =>
        {
            return !Input.GetMouseButton(0);
        });
#else
        yield return new WaitUntil(() =>
        {
            return Input.touchCount == 1;
        });
        yield return new WaitUntil(() =>
        {
            return Input.touchCount == 0;
        });
#endif
        jumpPlayerUI.SetActive(false);
        Time.timeScale = 1f;

        yield return new WaitForSeconds(3f);

        _player.Move(0.5f);
        _player.CanControl = false;
        Action hitEvent = () => playerHitted = true;
        _player.OnDamaged += hitEvent;

        GameObject spike = ObjectPoolManager.Instance.GetPooledGameObject(spikePrefab);
        spike.transform.position = new Vector3(0, 0, _player.transform.position.z + 60);

        yield return new WaitUntil(() => playerHitted);
        _player.OnDamaged -= hitEvent;

        yield return new WaitForSeconds(0.1f);
        hpUI.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        hpUI.SetActive(false);
        for (int i = 0; i < 45; i++)
        {
            GameObject gem = ObjectPoolManager.Instance.GetPooledGameObject(gemPrefab);
            gem.transform.position = _player.transform.position + new Vector3(0, 0, 60 + i);
        }
        yield return new WaitForSeconds(8f);

        gemUI.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        gemUI.SetActive(false);

        chaser.Timer = 10;
        Action<float> chaserEvent = (_) => playerHittedByChaser = true;
        _player.OnChangedBoostGazy += chaserEvent;
        yield return new WaitUntil(() => playerHittedByChaser);
        _player.OnChangedBoostGazy -= chaserEvent;

        gemUI.SetActive(true);
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1f;
        gemUI.SetActive(false);


        Instantiate(spikePrefab, _player.transform.position + new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(spikePrefab, _player.transform.position + new Vector3(0, 0, 80), Quaternion.identity);
        Instantiate(spikePrefab, _player.transform.position + new Vector3(0, 0, 100), Quaternion.identity);

        playerSetting.playerPrefabs[0].GetComponent<Player>().PlayerLevel = 1;

        yield return null;
    }
}
