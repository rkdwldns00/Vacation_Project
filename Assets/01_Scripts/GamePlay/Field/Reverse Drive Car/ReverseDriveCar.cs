using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseDriveCar : MonoBehaviour
{
    [SerializeField] private GameObject[] _carModels;
    [SerializeField] private float _minMoveSpeed;
    [SerializeField] private float _maxMoveSpeed;
    [SerializeField] private float _startTime;

    private float _moveSpeed;

    private void Awake()
    {
        int randCarIndex = Random.Range(0, _carModels.Length);
        _carModels[randCarIndex].SetActive(true);

        _moveSpeed = Random.Range(_minMoveSpeed, _maxMoveSpeed);
    }

    private void Update()
    {
        if (Player.Instance != null && transform.position.z + 20 < Player.Instance.transform.position.z)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (Player.Instance != null && transform.position.z <= Player.Instance.transform.position.z + _startTime * Player.Instance.MoveSpeed)
        {
            transform.Translate(Vector3.back * _moveSpeed * Time.fixedDeltaTime);
        }
    }
}
