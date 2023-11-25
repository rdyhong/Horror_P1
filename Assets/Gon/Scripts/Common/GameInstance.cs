using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
    static PlayerController _playerController = null;

    public static void SpawnPlayer()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    public static Vector3 GetPlayerPosition()
    {
        if (_playerController == null) return Vector3.zero;

        return _playerController.transform.position;
    }
    public static Transform GetPlayerTransform()
    {
        if (_playerController == null) return null;

        return _playerController.transform;
    }
}
