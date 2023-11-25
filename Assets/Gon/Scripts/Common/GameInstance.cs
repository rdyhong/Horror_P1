using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
    static PlayerController _playerController;
    public static PlayerController s_PlayerController => s_PlayerController; 

    public static void SpawnPlayer()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }
}
