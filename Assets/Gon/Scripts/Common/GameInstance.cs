using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
    public static PlayerController PlayerController => _playerController;
    static PlayerController _playerController = null;

    public static ItemData ItemData => _itemData;
    static ItemData _itemData;

    protected override void Awake()
    {
        base.Awake();

        _itemData = new ItemData();
        UserData.Init();
    }

    private void Start()
    {
        
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
