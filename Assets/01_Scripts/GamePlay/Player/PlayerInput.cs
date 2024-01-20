using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float xRate = Input.mousePosition.x / Screen.width;
            _player.Move(xRate);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _player.Jump();
        }
    }
}
