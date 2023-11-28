using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
    static PlayerController _playerController = null;
    public static PlayerController PlayerController => _playerController;

    protected override void Awake()
    {
        base.Awake();

        UserData.Init();
    }

    public void SetPlayer(PlayerController pc)
    {
        _playerController = pc;

        if (!UIMgr.Inst.IsPanelOpened<PlayerBasePanel>())
        {
            UIMgr.Inst.Push<PlayerBasePanel>();
        }
    }

    public static Vector3 GetPlayerPosition()
    {
        if (PlayerController == null) return Vector3.zero;
        else return PlayerController.transform.position;
    }
    public static Transform GetPlayerTransform()
    {
        if (PlayerController == null) return null;
        else return PlayerController.transform;
    }
}
