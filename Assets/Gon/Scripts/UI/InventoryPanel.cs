using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : UIRoot
{
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
}
