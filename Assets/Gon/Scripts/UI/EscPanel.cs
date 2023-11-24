using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscPanel : UIRoot
{
    [SerializeField] Button _btnSetting;

    [SerializeField] Button _btnExit;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        _btnSetting.onClick.AddListener(() => { OnClickSetting(); });
        _btnExit.onClick.AddListener(() => { OnClickExit(); });
    }

    public override void Push()
    {
        base.Push();
        InputMgr.SetCursorAvtive(true);
        InputMgr.StopPlayerMove(true);
        Time.timeScale = 0;
    }

    public override void Pop()
    {
        base.Pop();
        InputMgr.SetCursorAvtive(false);
        InputMgr.StopPlayerMove(false);
        Time.timeScale = 1;
    }

    void OnClickSetting()
    {

    }

    void OnClickExit()
    {
        Application.Quit();
    }
}
