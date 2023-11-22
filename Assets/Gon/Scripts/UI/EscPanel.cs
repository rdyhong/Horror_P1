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
    }

    public override void Pop()
    {
        base.Pop();
        InputMgr.SetCursorAvtive(false);
    }

    void OnClickSetting()
    {

    }

    void OnClickExit()
    {
        Application.Quit();
    }
}
