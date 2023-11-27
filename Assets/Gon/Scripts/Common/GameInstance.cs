using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
    static PlayerController _playerController = null;

    protected override void Awake()
    {
        base.Awake();

        UserData.Init();
    }

    public void SpawnPlayer()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public static Vector3 GetPlayerPosition()
    {
        if (_playerController == null)
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        if (_playerController == null) return Vector3.zero;
        else return _playerController.transform.position;
    }
    public static Transform GetPlayerTransform()
    {
        if (_playerController == null)
        {
            _playerController = FindObjectOfType<PlayerController>();
        }

        if (_playerController == null) return null;
        else return _playerController.transform;
    }
}
