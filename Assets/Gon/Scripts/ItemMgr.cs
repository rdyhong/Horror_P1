using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMgr : Singleton<ItemMgr>
{
    public List<Item> _onFieldItems = new List<Item>();

    List<int> _ownedItems = new List<int>();
    int _onHandItem = -1;

    protected override void Awake()
    {
        base.Awake();

        Init();
    }

    void Init()
    {

    }

    public T GetItemObject<T>() where T : Object
    {


        return ResourcesMgr.Inst.Spawn<T>(EResourcePath.Item);
    }
}
