using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : UIRoot
{
    [SerializeField] Button _btnReset;

    private void Awake()
    {
        _btnReset.onClick.AddListener(() => {
            PlayerPrefsHelper.ResetData();
        });
    }

    public override void Push()
    {
        base.Push();

    }
    public override void Pop()
    {
        base.Pop();

    }
}
