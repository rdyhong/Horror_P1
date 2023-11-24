using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasePanel : UIRoot
{
    [SerializeField] Text subTitleT;
    private void Awake()
    {
        
    }

    private void Update()
    {
        if(InputMgr.KeyDown(KeyCode.Escape))
        {
            if (SceneMgr.GetCurrentScene() != ESceneType.Main) return;

            UIMgr.Inst.Push<EscPanel>();
        }
    }
}
