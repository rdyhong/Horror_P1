using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMgr : Singleton<ItemMgr>
{
    

    public T GetItemObject<T>() where T : Object
    {


        return ResourcesMgr.Inst.Spawn<T>(EResourcePath.Item);
    }
}
