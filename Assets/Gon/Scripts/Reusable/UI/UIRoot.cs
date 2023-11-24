using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIRoot : MonoBehaviour
{
    public bool _isEscLock = false;
    public EUIType uiType;

    bool _isLoaded = false;

    public virtual void Push()
    {
        
    }

    public virtual void Pop()
    {
        UIMgr.Inst.Pop(this);
    }
}
