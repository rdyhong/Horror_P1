using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public bool _isCloseBlock = false;

    bool _isLoaded = false;

    public virtual void Push(bool isCloseBlock = false)
    {
        _isCloseBlock = isCloseBlock;
        UIMgr.Inst.Push(this);
    }

    public virtual void Pop()
    {
        UIMgr.Inst.Pop();
    }
}
