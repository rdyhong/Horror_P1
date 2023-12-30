using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : Singleton<GameInstance>
{
    public static int MainStep = 0;
    public static PlayerController PlayerController => _playerController;
    static PlayerController _playerController = null;

    protected override void Awake()
    {
        base.Awake();

        JsonMgr.Inst.Init();
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

    public int GetMainStep()
    {
        int mainStep = PlayerPrefsHelper.GetInt(PlayerPrefsHelper.PPKEY_MAIN_STEP);

        if(mainStep < 0)
        {
            PlayerPrefsHelper.SaveInt(PlayerPrefsHelper.PPKEY_MAIN_STEP, 0);
            mainStep = 0;
        }

        return mainStep;
    }

    public void IncreaseMainStep()
    {
        int mainStep = PlayerPrefsHelper.GetInt(PlayerPrefsHelper.PPKEY_MAIN_STEP);

        if (mainStep < 0)
        {
            mainStep = 0;
        }

        PlayerPrefsHelper.SaveInt(PlayerPrefsHelper.PPKEY_MAIN_STEP, mainStep + 1);
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
